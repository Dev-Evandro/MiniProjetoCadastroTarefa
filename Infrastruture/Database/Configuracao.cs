using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastruture.Database
{
    public static class Configuracao
    {
        public static string? NomeInstancia { get; private set; }
        public static string? NomeUsuario { get; private set; }
        public static string? NomeSenha { get; private set; }
        public static string? NomeBanco { get; private set; }
        public static string? StringConexao { get; private set; }

        private static readonly string caminhoArquivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PDVFinanceiro\\Config\\config.xml");

        public static void CriarArquivoPadrao()
        {
            try
            {
                string? diretorio = Path.GetDirectoryName(caminhoArquivo);
                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);

                // Cria o XML padrão
                XDocument doc = new XDocument(
                    new XElement("configuracao",
                        new XElement("nomeInstancia", ""),
                        new XElement("nomeUsuario", ""),
                        new XElement("nomeSenha", ""),
                        new XElement("nomeBanco", "")
                    )
                );

                doc.Save(caminhoArquivo);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar arquivo de configuração padrão.", ex);
            }
        }

        public static void Carregar()
        {
            if (!File.Exists(caminhoArquivo))
            {
                CriarArquivoPadrao();
                throw new FileNotFoundException(
                    $"Arquivo de configuração não encontrado. Um novo foi criado em: \n {caminhoArquivo}"
                );
            }

            try
            {
                XDocument doc = XDocument.Load(caminhoArquivo);
                XElement root = doc.Root;
                if (root == null || root.Name != "configuracao")
                    throw new Exception("Arquivo de configuração inválido: nó <configuracao> não encontrado.");


                NomeInstancia = root.Element("nomeInstancia")?.Value ?? "";
                NomeUsuario = root.Element("nomeUsuario")?.Value ?? "";
                NomeSenha = root.Element("nomeSenha")?.Value ?? "";
                NomeBanco = root.Element("nomeBanco")?.Value ?? "";

                if (string.IsNullOrWhiteSpace(NomeUsuario))
                {
                    // Usa segurança integrada (sem usuário/senha)
                    StringConexao = $"Data Source={NomeInstancia};Initial Catalog={NomeBanco};Integrated Security=True;TrustServerCertificate=True";
                }
                else
                {
                    // Usa usuário/senha
                    StringConexao = $"Data Source={NomeInstancia};Initial Catalog={NomeBanco};User ID={NomeUsuario};Password={NomeSenha};TrustServerCertificate=True";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar arquivo de configuração.", ex);
            }
        }
    }
}
