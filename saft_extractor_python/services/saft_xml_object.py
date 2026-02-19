"""
Geração do ficheiro XML SAFT conforme schema SAFT-PT 1.04_01
"""
from lxml import etree
from datetime import datetime
from typing import List
from models.saft_models import SAFTHeader, Customer, Product, Invoice
from services.saft_xml_details import SAFTXMLDetails

class SAFTXMLObject:
    """Responsável pela construção e exportação do ficheiro SAFT em XML"""
    
    def __init__(self):
        self.details = SAFTXMLDetails()
    
    def generate_saft_file(self, output_path: str, start_date: datetime, end_date: datetime):
        """Gera o ficheiro SAFT XML completo"""
        print(f"\n{'='*60}")
        print(f"Gerando ficheiro SAFT XML")
        print(f"{'='*60}")
        
        try:
            # Extrair dados
            fiscal_year = start_date.year
            header = self.details.get_header(fiscal_year, start_date, end_date)
            customers = self.details.get_customers()
            products = self.details.get_products()
            invoices = self.details.get_invoices(start_date, end_date)
            
            # Criar XML
            print(f"\n{'='*60}")
            print(f"Criando estrutura XML...")
            print(f"{'='*60}")
            
            # Namespace SAFT-PT
            nsmap = {
                None: "urn:OECD:StandardAuditFile-Tax:PT_1.04_01",
                "xsi": "http://www.w3.org/2001/XMLSchema-instance"
            }
            
            root = etree.Element("AuditFile", nsmap=nsmap)
            
            # Header
            self._write_header(root, header)
            
            # MasterFiles
            self._write_master_files(root, customers, products)
            
            # SourceDocuments
            self._write_source_documents(root, invoices)
            
            # Salvar ficheiro
            tree = etree.ElementTree(root)
            tree.write(
                output_path,
                pretty_print=True,
                xml_declaration=True,
                encoding='UTF-8'
            )
            
            print(f"\n{'='*60}")
            print(f"✓ Ficheiro SAFT gerado com sucesso!")
            print(f"  Localização: {output_path}")
            print(f"{'='*60}\n")
            
        except Exception as e:
            print(f"\n✗ ERRO ao gerar ficheiro SAFT: {e}")
            raise
    
    def _write_header(self, parent: etree.Element, header: SAFTHeader):
        """Escreve o Header do SAFT"""
        print("\nCriando Header...")
        
        header_elem = etree.SubElement(parent, "Header")
        
        # Campos obrigatórios
        etree.SubElement(header_elem, "AuditFileVersion").text = header.AuditFileVersion
        etree.SubElement(header_elem, "CompanyID").text = header.CompanyID
        etree.SubElement(header_elem, "TaxRegistrationNumber").text = header.TaxRegistrationNumber
        etree.SubElement(header_elem, "TaxAccountingBasis").text = header.TaxAccountingBasis
        etree.SubElement(header_elem, "CompanyName").text = header.CompanyName
        
        # BusinessAddress
        if header.BusinessAddress:
            addr_elem = etree.SubElement(header_elem, "BusinessAddress")
            if header.BusinessAddress.BuildingNumber:
                etree.SubElement(addr_elem, "BuildingNumber").text = header.BusinessAddress.BuildingNumber
            if header.BusinessAddress.StreetName:
                etree.SubElement(addr_elem, "StreetName").text = header.BusinessAddress.StreetName
            etree.SubElement(addr_elem, "AddressDetail").text = header.BusinessAddress.AddressDetail
            etree.SubElement(addr_elem, "City").text = header.BusinessAddress.City
            etree.SubElement(addr_elem, "PostalCode").text = header.BusinessAddress.PostalCode
            if header.BusinessAddress.Region:
                etree.SubElement(addr_elem, "Region").text = header.BusinessAddress.Region
            etree.SubElement(addr_elem, "Country").text = header.BusinessAddress.Country
        
        # Datas
        etree.SubElement(header_elem, "FiscalYear").text = str(header.FiscalYear)
        etree.SubElement(header_elem, "StartDate").text = header.StartDate.strftime('%Y-%m-%d')
        etree.SubElement(header_elem, "EndDate").text = header.EndDate.strftime('%Y-%m-%d')
        etree.SubElement(header_elem, "CurrencyCode").text = header.CurrencyCode
        etree.SubElement(header_elem, "DateCreated").text = header.DateCreated.strftime('%Y-%m-%d')
        etree.SubElement(header_elem, "TaxEntity").text = header.TaxEntity
        etree.SubElement(header_elem, "ProductCompanyTaxID").text = header.ProductCompanyTaxID
        etree.SubElement(header_elem, "SoftwareCertificateNumber").text = header.SoftwareCertificateNumber
        etree.SubElement(header_elem, "ProductID").text = header.ProductID
        etree.SubElement(header_elem, "ProductVersion").text = header.ProductVersion
        
        # Campos opcionais
        if header.HeaderComment:
            etree.SubElement(header_elem, "HeaderComment").text = header.HeaderComment
        if header.Telephone:
            etree.SubElement(header_elem, "Telephone").text = header.Telephone
        if header.Fax:
            etree.SubElement(header_elem, "Fax").text = header.Fax
        if header.Email:
            etree.SubElement(header_elem, "Email").text = header.Email
        if header.Website:
            etree.SubElement(header_elem, "Website").text = header.Website
        
        print(f"  ✓ Header criado:")
        print(f"    CompanyName: {header.CompanyName}")
        print(f"    ProductID: {header.ProductID}")
        print(f"    ProductVersion: {header.ProductVersion}")
    
    def _write_master_files(self, parent: etree.Element, customers: List[Customer], products: List[Product]):
        """Escreve MasterFiles (Customers + Products)"""
        print(f"\nCriando MasterFiles...")
        
        master_elem = etree.SubElement(parent, "MasterFiles")
        
        # Customers
        for customer in customers:
            cust_elem = etree.SubElement(master_elem, "Customer")
            etree.SubElement(cust_elem, "CustomerID").text = customer.CustomerID
            etree.SubElement(cust_elem, "AccountID").text = customer.AccountID
            etree.SubElement(cust_elem, "CustomerTaxID").text = customer.CustomerTaxID
            etree.SubElement(cust_elem, "CompanyName").text = customer.CompanyName
            
            # BillingAddress
            addr_elem = etree.SubElement(cust_elem, "BillingAddress")
            if customer.BuildingNumber:
                etree.SubElement(addr_elem, "BuildingNumber").text = customer.BuildingNumber
            if customer.StreetName:
                etree.SubElement(addr_elem, "StreetName").text = customer.StreetName
            etree.SubElement(addr_elem, "AddressDetail").text = customer.AddressDetail
            etree.SubElement(addr_elem, "City").text = customer.City
            etree.SubElement(addr_elem, "PostalCode").text = customer.PostalCode
            if customer.Region:
                etree.SubElement(addr_elem, "Region").text = customer.Region
            etree.SubElement(addr_elem, "Country").text = customer.Country
            
            etree.SubElement(cust_elem, "SelfBillingIndicator").text = str(customer.SelfBillingIndicator)
        
        print(f"  ✓ {len(customers)} clientes adicionados")
        
        # Products
        for product in products:
            prod_elem = etree.SubElement(master_elem, "Product")
            etree.SubElement(prod_elem, "ProductType").text = product.ProductType
            etree.SubElement(prod_elem, "ProductCode").text = product.ProductCode
            if product.ProductGroup:
                etree.SubElement(prod_elem, "ProductGroup").text = product.ProductGroup
            etree.SubElement(prod_elem, "ProductDescription").text = product.ProductDescription
            etree.SubElement(prod_elem, "ProductNumberCode").text = product.ProductNumberCode
        
        print(f"  ✓ {len(products)} produtos adicionados")
    
    def _write_source_documents(self, parent: etree.Element, invoices: List[Invoice]):
        """Escreve SourceDocuments (Sales Invoices)"""
        print(f"\nCriando SourceDocuments...")
        
        source_elem = etree.SubElement(parent, "SourceDocuments")
        sales_elem = etree.SubElement(source_elem, "SalesInvoices")
        
        etree.SubElement(sales_elem, "NumberOfEntries").text = str(len(invoices))
        
        total_debit = sum(inv.GrossTotal for inv in invoices)
        total_credit = 0  # Ajustar conforme necessário
        
        etree.SubElement(sales_elem, "TotalDebit").text = f"{total_debit:.2f}"
        etree.SubElement(sales_elem, "TotalCredit").text = f"{total_credit:.2f}"
        
        # Invoices
        for invoice in invoices:
            inv_elem = etree.SubElement(sales_elem, "Invoice")
            etree.SubElement(inv_elem, "InvoiceNo").text = invoice.InvoiceNo
            etree.SubElement(inv_elem, "ATCUD").text = invoice.ATCUD
            
            # DocumentStatus
            status_elem = etree.SubElement(inv_elem, "DocumentStatus")
            etree.SubElement(status_elem, "InvoiceStatus").text = invoice.DocumentStatus
            etree.SubElement(status_elem, "InvoiceStatusDate").text = invoice.SystemEntryDate.strftime('%Y-%m-%dT%H:%M:%S')
            etree.SubElement(status_elem, "SourceID").text = invoice.SourceID
            etree.SubElement(status_elem, "SourceBilling").text = invoice.SourceBilling or "P"
            
            etree.SubElement(inv_elem, "Hash").text = invoice.Hash
            etree.SubElement(inv_elem, "HashControl").text = invoice.HashControl
            if invoice.Period:
                etree.SubElement(inv_elem, "Period").text = str(invoice.Period)
            etree.SubElement(inv_elem, "InvoiceDate").text = invoice.InvoiceDate.strftime('%Y-%m-%d')
            etree.SubElement(inv_elem, "InvoiceType").text = invoice.InvoiceType
            
            # SpecialRegimes
            regimes_elem = etree.SubElement(inv_elem, "SpecialRegimes")
            etree.SubElement(regimes_elem, "SelfBillingIndicator").text = "0"
            etree.SubElement(regimes_elem, "CashVATSchemeIndicator").text = "0"
            etree.SubElement(regimes_elem, "ThirdPartiesBillingIndicator").text = "0"
            
            etree.SubElement(inv_elem, "SourceID").text = invoice.SourceID
            if invoice.EACCode:
                etree.SubElement(inv_elem, "EACCode").text = invoice.EACCode
            etree.SubElement(inv_elem, "SystemEntryDate").text = invoice.SystemEntryDate.strftime('%Y-%m-%dT%H:%M:%S')
            etree.SubElement(inv_elem, "CustomerID").text = invoice.CustomerID
            
            # Lines (adicionar se necessário)
            
            # DocumentTotals
            totals_elem = etree.SubElement(inv_elem, "DocumentTotals")
            etree.SubElement(totals_elem, "TaxPayable").text = f"{invoice.TaxPayable:.2f}"
            etree.SubElement(totals_elem, "NetTotal").text = f"{invoice.NetTotal:.2f}"
            etree.SubElement(totals_elem, "GrossTotal").text = f"{invoice.GrossTotal:.2f}"
        
        print(f"  ✓ {len(invoices)} faturas adicionadas")
