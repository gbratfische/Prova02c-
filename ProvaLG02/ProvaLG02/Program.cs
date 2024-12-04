using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProvaLG02
{
    internal class Program
    {
        private const string ARQUIVO_CAMINHO = @"C:\prova02\";
        private const string ARQUIVO_NOME = @"contatos.txt";
        private const string ARQUIVO = ARQUIVO_CAMINHO + ARQUIVO_NOME;

        private static List<Contato> listaContatos = new List<Contato>();
        static void Main(string[] args)
        {
            CarregarContatos();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Digite a opção desejada:\n1-Cadastrar Novo Contato\n2-Mostrar todos os contatos\n3-Localizar contato (nome)\n4-Localizar contato (telefone)\n5-Sair");
                if (int.TryParse(Console.ReadLine(), out int opcao))
                {
                    switch (opcao)
                    {
                        case 1:
                            CriarContato();
                            break;
                        case 2:
                            Listar(listaContatos);
                            break;
                        case 3:
                            BuscaNome();
                            break;
                        case 4:
                            BuscaTelefone();
                            break;
                        case 5:
                            SalvarContato();
                            Console.WriteLine("Fim do programa!");
                            return;
                        default:
                            Console.WriteLine("Opção inválida, tente novamente!");
                            break;
                    }

                }
                Console.WriteLine("Pressione uma tecla para sair...");
                Console.ReadKey();
            }
        }

        private static void CarregarContatos()
        {
            if (File.Exists(ARQUIVO))
            {
                try
                {
                    string[] linhas = File.ReadAllLines(ARQUIVO);
                    foreach (string linha in linhas)
                    {
                        string[] dados = linha.Split(';');
                        if (dados.Length == 3 && DateTime.TryParse(dados[2], out DateTime data))
                        {
                            listaContatos.Add(new Contato
                            {
                                Nome = dados[0],
                                Telefone = dados[1],
                                DataNasc = data
                            });
                        }
                    }
                    Console.WriteLine($"Foram carregados {listaContatos.Count} contatos do arquivo.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao carregar produtos: {ex.Message}");
                }
                Console.ReadKey();
            }
        }
        private static void Listar(List<Contato> listaContatos)
        {
            Console.Clear();
            Console.WriteLine("*** LISTA DE CONTATOS ***\n ");
            listaContatos.Sort((cont1, cont2) => cont1.Nome.CompareTo(cont2.Nome));
            foreach (var contato in listaContatos)
            {
                Console.WriteLine($"Nome: {contato.Nome}");
                Console.WriteLine($"Telefone: {contato.Telefone}");
                Console.WriteLine($"Data de Nascimento: {contato.DataNasc:dd/MM/yyyy}\n");
            }
        }

        private static void CriarContato()
        {
            Console.Clear();
            Console.WriteLine("*** CADASTRO DE CONTATO ***\n");

            try
            {
                Contato contato = new Contato();

                Console.Write("Digite o nome do contato: ");
                contato.Nome = Console.ReadLine();

                Console.Write("Digite o telefone do contato: ");
                contato.Telefone = Console.ReadLine();

                Console.Write("Digite a data de nascimento do contato: ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime data))
                {
                    contato.DataNasc = data;
                    listaContatos.Add(contato);
                    Console.WriteLine("\nContato cadastrado com sucesso!");
                }
                else
                {
                    Console.WriteLine("\nData inválida!");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\nErro no cadastro: {ex.Message}");
            }
        }

        private static void BuscaNome()
        {
            Console.Clear();
            Console.WriteLine("*** LOCALIZAR PRODUTO PELO NOME ***\n");

            Console.Write("Digite o nome do contato: ");
            string nomeContato = Console.ReadLine() ?? "";

            List<Contato> contatoEncontrado = new List<Contato>();
            foreach (Contato cont in listaContatos)
            {
                if (cont.Nome == nomeContato)
                {
                    contatoEncontrado.Add(cont);
                }
            }

            if (contatoEncontrado.Any())
            {
                Console.WriteLine("\nContatos encontrados:");
                foreach (var contato in contatoEncontrado)
                {
                    Console.WriteLine($"Nome: {contato.Nome}");
                    Console.WriteLine($"Telefone: {contato.Telefone}");
                    Console.WriteLine($"Data de Nascimento: {contato.DataNasc:dd/MM/yyyy}\n");
                }
            }
            else
            {
                Console.WriteLine("\nNenhum contato encontrado com este nome.");
            }
        }

        private static void BuscaTelefone()
        {
            Console.Clear();
            Console.WriteLine("*** LOCALIZAR CONTATO PELO TELEFONE ***\n");

            Console.Write("Digite o telefone do contato: ");
            string telefoneContato = Console.ReadLine() ?? "";

            List<Contato> contatoEncontrado = new List<Contato>();
            foreach (Contato cont in listaContatos)
            {
                if (cont.Telefone == telefoneContato)
                {
                    contatoEncontrado.Add(cont);
                }
            }

            if (contatoEncontrado.Any())
            {
                Console.WriteLine("\nContatos encontrados:");
                foreach (var contato in contatoEncontrado)
                {
                    Console.WriteLine($"Nome: {contato.Nome}");
                    Console.WriteLine($"Telefone: {contato.Telefone}");
                    Console.WriteLine($"Data de Nascimento: {contato.DataNasc:dd/MM/yyyy} \n");
                }
            }
            else
            {
                Console.WriteLine("\nNenhum contato encontrado com este telefone.");
            }
        }

        private static void SalvarContato()
        {
            try
            {
                string diretorio = ARQUIVO_CAMINHO;
                if (Directory.Exists(diretorio) == false)
                {
                    Directory.CreateDirectory(diretorio);
                }

                File.WriteAllLines(ARQUIVO, listaContatos.Select(cont => cont.ToString()));
                Console.WriteLine("\nContatos salvos com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao salvar contatos: {ex.Message}");
            }
            Console.ReadKey();
        }
    }
    
}
