"""
Modelos de dados para o SAFT
"""
from dataclasses import dataclass, field
from datetime import datetime
from typing import Optional, List
from decimal import Decimal

@dataclass
class CompanyAddress:
    """Endereço da empresa"""
    AddressDetail: str = ""
    City: str = ""
    PostalCode: str = ""
    Country: str = "PT"
    BuildingNumber: Optional[str] = None
    StreetName: Optional[str] = None
    Region: Optional[str] = None

@dataclass
class SAFTHeader:
    """Cabeçalho do ficheiro SAFT"""
    # Campos principais
    AuditFileVersion: str = "1.04_01"
    CompanyID: str = ""
    TaxRegistrationNumber: str = ""
    TaxAccountingBasis: str = "F"  # F=Faturação
    CompanyName: str = ""
    
    # Endereço
    BusinessAddress: Optional[CompanyAddress] = None
    
    # Datas
    FiscalYear: int = 0
    StartDate: datetime = field(default_factory=datetime.now)
    EndDate: datetime = field(default_factory=datetime.now)
    DateCreated: datetime = field(default_factory=datetime.now)
    
    # Outros
    CurrencyCode: str = "EUR"
    TaxEntity: str = "Global"
    ProductCompanyTaxID: str = ""
    SoftwareCertificateNumber: str = ""
    ProductID: str = "PULSE DOMINOS"
    ProductVersion: str = "1.0"
    
    # Campos adicionais do spGetSAFTDetails
    HeaderComment: Optional[str] = None
    Telephone: Optional[str] = None
    Fax: Optional[str] = None
    Email: Optional[str] = None
    Website: Optional[str] = None

@dataclass
class Customer:
    """Cliente do SAFT"""
    CustomerID: str = ""
    AccountID: str = ""
    CustomerTaxID: str = ""
    CompanyName: str = ""
    AddressDetail: str = ""
    City: str = ""
    PostalCode: str = ""
    Country: str = "PT"
    SelfBillingIndicator: int = 0
    
    # Opcionais
    Contact: Optional[str] = None
    BuildingNumber: Optional[str] = None
    StreetName: Optional[str] = None
    Region: Optional[str] = None
    Telephone: Optional[str] = None
    Fax: Optional[str] = None
    Email: Optional[str] = None
    Website: Optional[str] = None

@dataclass
class Product:
    """Produto/Artigo do SAFT"""
    ProductType: str = "P"  # P=Produto, S=Serviço
    ProductCode: str = ""
    ProductDescription: str = ""
    ProductNumberCode: str = ""
    ProductGroup: Optional[str] = None

@dataclass
class InvoiceLine:
    """Linha de fatura"""
    LineNumber: int = 0
    ProductCode: str = ""
    ProductDescription: str = ""
    Quantity: Decimal = Decimal("0")
    UnitOfMeasure: str = "UN"
    UnitPrice: Decimal = Decimal("0")
    TaxType: str = "IVA"
    TaxCountryRegion: str = "PT"
    TaxCode: str = "NOR"
    TaxPercentage: Decimal = Decimal("0")
    CreditAmount: Decimal = Decimal("0")
    
    # Campos de isenção (opcionais)
    TaxExemptionReason: Optional[str] = None
    TaxExemptionCode: Optional[str] = None

@dataclass
class DocumentTotals:
    """Totais do documento"""
    TaxPayable: Decimal = Decimal("0")
    NetTotal: Decimal = Decimal("0")
    GrossTotal: Decimal = Decimal("0")

@dataclass
class Invoice:
    """Fatura do SAFT"""
    InvoiceNo: str = ""
    ATCUD: str = "0"
    DocumentStatus: str = "N"  # N=Normal, A=Anulado
    Hash: str = ""
    HashControl: str = "1"
    InvoiceDate: datetime = field(default_factory=datetime.now)
    InvoiceType: str = "FT"
    SourceID: str = ""
    SystemEntryDate: datetime = field(default_factory=datetime.now)
    CustomerID: str = ""
    
    # Totais
    TaxPayable: Decimal = Decimal("0")
    NetTotal: Decimal = Decimal("0")
    GrossTotal: Decimal = Decimal("0")
    
    # Linhas da fatura
    Lines: List[InvoiceLine] = field(default_factory=list)
    
    # Opcionais
    Period: Optional[int] = None
    SourceBilling: Optional[str] = None
    EACCode: Optional[str] = None
