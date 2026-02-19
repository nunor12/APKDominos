"""
Extração de dados das Stored Procedures do PULSE DOMINOS
Esta classe é crítica - é responsável por toda a extração de dados
"""
import pyodbc
from datetime import datetime
from decimal import Decimal
from typing import List, Dict
from models.saft_models import (
    SAFTHeader, CompanyAddress, Customer, Product, 
    Invoice, InvoiceLine, DocumentTotals
)
from utils.pulse_connection import PulseConnection

class SAFTXMLDetails:
    """Extrai dados das SPs e monta estrutura SAFT"""
    
    def __init__(self):
        self.location_code = PulseConnection.get_location_code()
        self.command_timeout = PulseConnection.get_command_timeout()
    
    def get_header(self, fiscal_year: int, start_date: datetime, end_date: datetime) -> SAFTHeader:
        """
        Obtém header combinando dados de 2 SPs:
        1. spGetSAFTDetails - Retorna Setting/Value (quase todo o header)
        2. spGetSAFTXMLHeaderDetails - Retorna ProductVersion
        """
        header = SAFTHeader()
        header.FiscalYear = fiscal_year
        header.StartDate = start_date
        header.EndDate = end_date
        header.DateCreated = datetime.now()
        
        conn = PulseConnection.create_connection()
        cursor = conn.cursor()
        
        try:
            # 1. Obter dados principais do spGetSAFTDetails
            print(f"Executando spGetSAFTDetails com LocationCode: {self.location_code}")
            cursor.execute(
                "{CALL dbo.spGetSAFTDetails(?)}",
                self.location_code
            )
            
            # A SP retorna pares Setting/Value
            # Vamos construir um dicionário
            saft_details = {}
            for row in cursor.fetchall():
                setting = row[0]  # Setting
                value = row[1]    # Value
                saft_details[setting] = value
                print(f"  {setting} = {value}")
            
            # Mapear campos do spGetSAFTDetails para o header
            header.AuditFileVersion = saft_details.get('AuditFileVersion', '1.04_01')
            header.CompanyID = saft_details.get('CompanyID', '')
            header.TaxRegistrationNumber = saft_details.get('TaxRegistrationNumber', '')
            header.TaxAccountingBasis = saft_details.get('TaxAccountingBasis', 'F')
            header.CompanyName = saft_details.get('CompanyName', '')
            header.CurrencyCode = saft_details.get('CurrencyCode', 'EUR')
            header.TaxEntity = saft_details.get('TaxEntity', 'Global')
            header.ProductCompanyTaxID = saft_details.get('ProductCompanyTaxID', '')
            header.SoftwareCertificateNumber = saft_details.get('SoftwareCertificateNumber', '')
            header.ProductID = saft_details.get('ProductID', 'PULSE DOMINOS')
            
            # Campos opcionais
            header.HeaderComment = saft_details.get('HeaderComment')
            header.Telephone = saft_details.get('Telephone')
            header.Fax = saft_details.get('Fax')
            header.Email = saft_details.get('Email')
            header.Website = saft_details.get('Website')
            
            # Endereço da empresa
            address = CompanyAddress()
            address.BuildingNumber = saft_details.get('BuildingNumber')
            address.StreetName = saft_details.get('StreetName')
            address.AddressDetail = saft_details.get('AddressDetail', '')
            address.City = saft_details.get('City', '')
            address.PostalCode = saft_details.get('PostalCode', '')
            address.Region = saft_details.get('Region')
            address.Country = saft_details.get('Country', 'PT')
            header.BusinessAddress = address
            
            # 2. Obter ProductVersion do spGetSAFTXMLHeaderDetails
            print(f"\nExecutando spGetSAFTXMLHeaderDetails com LocationCode: {self.location_code}")
            cursor.execute(
                "{CALL dbo.spGetSAFTXMLHeaderDetails(?)}",
                self.location_code
            )
            
            row = cursor.fetchone()
            if row:
                location_code_ret = row[0]  # LocationCode
                version = row[1]            # Version
                header.ProductVersion = version
                print(f"  LocationCode: {location_code_ret}")
                print(f"  Version (ProductVersion): {version}")
            else:
                print("  AVISO: spGetSAFTXMLHeaderDetails não retornou dados")
                header.ProductVersion = "1.0"
            
            print(f"\nHeader montado com sucesso!")
            print(f"  CompanyName: {header.CompanyName}")
            print(f"  ProductID: {header.ProductID}")
            print(f"  ProductVersion: {header.ProductVersion}")
            
        except pyodbc.Error as e:
            print(f"\nERRO SQL ao obter header: {e}")
            raise Exception(f"Erro ao obter header SAFT: {e}")
        finally:
            cursor.close()
            conn.close()
        
        return header
    
    def get_customers(self) -> List[Customer]:
        """Obtém clientes do spGetCustomersForSAFTXML"""
        customers = []
        
        conn = PulseConnection.create_connection()
        cursor = conn.cursor()
        
        try:
            print(f"\nExecutando spGetCustomersForSAFTXML...")
            cursor.execute(
                "{CALL dbo.spGetCustomersForSAFTXML(?)}",
                self.location_code
            )
            
            for row in cursor.fetchall():
                customer = Customer(
                    CustomerID=row[0] or "",
                    AccountID=row[1] or "",
                    CustomerTaxID=row[2] or "",
                    CompanyName=row[3] or "",
                    Contact=row[4] if row[4] else None,
                    Telephone=row[5] if row[5] else None,
                    Fax=row[6] if row[6] else None,
                    Email=row[7] if row[7] else None,
                    Website=row[8] if row[8] else None,
                    SelfBillingIndicator=row[9] if row[9] is not None else 0,
                    BuildingNumber=row[10] if row[10] else None,
                    StreetName=row[11] if row[11] else None,
                    AddressDetail=row[12] or "",
                    City=row[13] or "",
                    PostalCode=row[14] or "",
                    Region=row[15] if row[15] else None,
                    Country=row[16] or "PT"
                )
                customers.append(customer)
            
            print(f"  {len(customers)} clientes extraídos")
        
        except pyodbc.Error as e:
            print(f"ERRO ao obter clientes: {e}")
            raise
        finally:
            cursor.close()
            conn.close()
        
        return customers
    
    def get_products(self) -> List[Product]:
        """Obtém produtos do spGetProductsForSAFTXML"""
        products = []
        
        conn = PulseConnection.create_connection()
        cursor = conn.cursor()
        
        try:
            print(f"\nExecutando spGetProductsForSAFTXML...")
            cursor.execute(
                "{CALL dbo.spGetProductsForSAFTXML(?)}",
                self.location_code
            )
            
            for row in cursor.fetchall():
                product = Product(
                    ProductType=row[0] or "P",
                    ProductCode=row[1] or "",
                    ProductGroup=row[2] if row[2] else None,
                    ProductDescription=row[3] or "",
                    ProductNumberCode=row[1] or ""  # Usar ProductCode como NumberCode
                )
                products.append(product)
            
            print(f"  {len(products)} produtos extraídos")
        
        except pyodbc.Error as e:
            print(f"ERRO ao obter produtos: {e}")
            raise
        finally:
            cursor.close()
            conn.close()
        
        return products
    
    def get_invoices(self, start_date: datetime, end_date: datetime) -> List[Invoice]:
        """Obtém faturas do spGetInvoicesForSAFTXML"""
        invoices = []
        
        conn = PulseConnection.create_connection()
        cursor = conn.cursor()
        
        try:
            print(f"\nExecutando spGetInvoicesForSAFTXML...")
            print(f"  Data início: {start_date.strftime('%Y-%m-%d')}")
            print(f"  Data fim: {end_date.strftime('%Y-%m-%d')}")
            
            cursor.execute(
                "{CALL dbo.spGetInvoicesForSAFTXML(?, ?, ?)}",
                self.location_code,
                start_date.strftime('%Y-%m-%d'),
                end_date.strftime('%Y-%m-%d')
            )
            
            for row in cursor.fetchall():
                invoice = Invoice(
                    InvoiceNo=row[0] or "",
                    ATCUD=row[1] or "0",
                    DocumentStatus=row[2] or "N",
                    Hash=row[3] or "",
                    HashControl=row[4] or "1",
                    Period=row[5] if row[5] is not None else None,
                    InvoiceDate=row[6] if row[6] else datetime.now(),
                    InvoiceType=row[7] or "FT",
                    SourceID=row[8] or "",
                    EACCode=row[9] if row[9] else None,
                    SystemEntryDate=row[10] if row[10] else datetime.now(),
                    CustomerID=row[11] or "",
                    SourceBilling=row[12] if row[12] else None,
                    TaxPayable=Decimal(str(row[13])) if row[13] is not None else Decimal("0"),
                    NetTotal=Decimal(str(row[14])) if row[14] is not None else Decimal("0"),
                    GrossTotal=Decimal(str(row[15])) if row[15] is not None else Decimal("0")
                )
                
                # Carregar linhas da fatura (não implementado aqui - adicione se necessário)
                # invoice.Lines = self.get_invoice_lines(invoice.InvoiceNo)
                
                invoices.append(invoice)
            
            print(f"  {len(invoices)} faturas extraídas")
        
        except pyodbc.Error as e:
            print(f"ERRO ao obter faturas: {e}")
            raise
        finally:
            cursor.close()
            conn.close()
        
        return invoices
