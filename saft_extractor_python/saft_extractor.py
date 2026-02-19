"""
SAFT Extractor Python - Extração de dados SAFT do PULSE DOMINOS
Script principal para geração de ficheiros SAFT XML
"""
import sys
import os
import argparse
from datetime import datetime

# Adicionar diretório raiz ao path para imports
sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))

from services.saft_xml_object import SAFTXMLObject
from utils.pulse_connection import PulseConnection

def parse_date(date_str: str) -> datetime:
    """Converte string de data para datetime"""
    try:
        return datetime.strptime(date_str, '%Y-%m-%d')
    except ValueError:
        try:
            return datetime.strptime(date_str, '%d/%m/%Y')
        except ValueError:
            raise ValueError(f"Formato de data inválido: {date_str}. Use YYYY-MM-DD ou DD/MM/YYYY")

def main():
    """Função principal"""
    parser = argparse.ArgumentParser(
        description='SAFT Extractor - Extração de dados SAFT do PULSE DOMINOS',
        formatter_class=argparse.RawDescriptionHelpFormatter,
        epilog="""
Exemplos de uso:
  python saft_extractor.py --start 2024-01-01 --end 2024-12-31
  python saft_extractor.py --start 01/01/2024 --end 31/12/2024 --output ./meu_saft.xml
  python saft_extractor.py --test-connection
        """
    )
    
    parser.add_argument(
        '--start', '-s',
        type=str,
        help='Data de início (YYYY-MM-DD ou DD/MM/YYYY)'
    )
    
    parser.add_argument(
        '--end', '-e',
        type=str,
        help='Data de fim (YYYY-MM-DD ou DD/MM/YYYY)'
    )
    
    parser.add_argument(
        '--output', '-o',
        type=str,
        help='Caminho do ficheiro XML de saída (padrão: output/SAFT_YYYYMMDD_YYYYMMDD.xml)'
    )
    
    parser.add_argument(
        '--test-connection', '-t',
        action='store_true',
        help='Testa a conexão com a base de dados e sai'
    )
    
    parser.add_argument(
        '--config', '-c',
        type=str,
        default='config.ini',
        help='Caminho do ficheiro de configuração (padrão: config.ini)'
    )
    
    args = parser.parse_args()
    
    # Carregar configuração
    PulseConnection.load_config(args.config)
    
    # Teste de conexão
    if args.test_connection:
        print("\n" + "="*60)
        print("Testando conexão com PULSE DOMINOS...")
        print("="*60)
        
        success, message = PulseConnection.test_connection()
        print(f"\n{message}\n")
        
        if success:
            location_code = PulseConnection.get_location_code()
            print(f"LocationCode configurado: {location_code}")
        
        sys.exit(0 if success else 1)
    
    # Validar argumentos
    if not args.start or not args.end:
        parser.print_help()
        print("\nERRO: As datas --start e --end são obrigatórias!")
        sys.exit(1)
    
    try:
        # Converter datas
        start_date = parse_date(args.start)
        end_date = parse_date(args.end)
        
        # Validar datas
        if end_date < start_date:
            print("ERRO: Data de fim não pode ser anterior à data de início!")
            sys.exit(1)
        
        # Definir output path
        if args.output:
            output_path = args.output
        else:
            # Criar diretório output se não existir
            os.makedirs('output', exist_ok=True)
            filename = f"SAFT_{start_date.strftime('%Y%m%d')}_{end_date.strftime('%Y%m%d')}.xml"
            output_path = os.path.join('output', filename)
        
        # Banner
        print("\n" + "="*60)
        print("SAFT Extractor - PULSE DOMINOS")
        print("="*60)
        print(f"Data início: {start_date.strftime('%d/%m/%Y')}")
        print(f"Data fim: {end_date.strftime('%d/%m/%Y')}")
        print(f"Ficheiro saída: {output_path}")
        print(f"LocationCode: {PulseConnection.get_location_code()}")
        print("="*60)
        
        # Gerar SAFT
        generator = SAFTXMLObject()
        generator.generate_saft_file(output_path, start_date, end_date)
        
        print("\n✓ Processo concluído com sucesso!\n")
        sys.exit(0)
        
    except Exception as e:
        print(f"\n✗ ERRO: {e}\n")
        import traceback
        traceback.print_exc()
        sys.exit(1)

if __name__ == '__main__':
    main()
