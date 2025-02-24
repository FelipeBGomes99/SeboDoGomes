using SeboDoGomesV2.Models;
using SeboDoGomesV2.Service;


internal class Menu
{
    public static void Main()
    {
        //carregando caminho dos arquivos json
        string bookPath = FileLoader.GetFilePath("Books.json");
        string clientPath = FileLoader.GetFilePath("Clients.json");


        //Desserializando dados e atribuindo a listas
        List<Book> catalog = FileLoader.LoadData<Book>(bookPath);
        List<Client> clients = FileLoader.LoadData<Client>(clientPath);


        int op;
        do
        {
            Console.Clear();
            Console.WriteLine(@"

        ▐▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▌
        ▐ ____       _                 _          ____                           ▌
        ▐/ ___|  ___| |__   ___     __| | ___    / ___| ___  _ __ ___   ___  ___ ▌
        ▐\___ \ / _ \ '_ \ / _ \   / _` |/ _ \  | |  _ / _ \| '_ ` _ \ / _ \/ __|▌
        ▐ ___) |  __/ |_) | (_) | | (_| | (_) | | |_| | (_) | | | | | |  __/\__ \▌
        ▐|____/ \___|_.__/ \___/   \__,_|\___/   \____|\___/|_| |_| |_|\___||___/▌
        ▐▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▌
        ");
            Console.WriteLine("1- Registrar um livro.\n" +
                              "2- Excluir um livro.\n" +
                              "3- Aluguel de livro.\n" +
                              "4- Devolução de livro.\n" +
                              "5- Registrar cliente.\n" +
                              "6- Excluir cliente.\n" +
                              "7- Mostrar livros\n" +
                              "8- Mostrar clientes\n" +
                              "9- Mostrar livros alugados por clientes\n" +
                              "0- Sair...\n");

            Console.Write("Digite uma das opções: ");
            op = int.Parse(Console.ReadLine()!);

            switch (op)
            {

                case 1:
                    ExibirTituloDaOpcao("Registrar livro");
                    Book.RegisterBook(catalog);
                    SucessMessage("Livro registrado!!");
                    break;
                case 2:
                    ExibirTituloDaOpcao("Excluir livro");
                    Book.RemoveBook(catalog);
                    break;
                case 3:
                    ExibirTituloDaOpcao("Aluguel de livro");
                    Book.BookRental(catalog, clients);
                    break;
                case 4:
                    ExibirTituloDaOpcao("Devoluçao de livro");
                    Book.ReturnBook(catalog, clients);
                    break;
                case 5:
                    ExibirTituloDaOpcao("Registrar cliente");
                    Client.RegisterClient(clients);
                    SucessMessage("Cliente registrado!!");
                    break;
                case 6:
                    ExibirTituloDaOpcao("Excluir cliente.");
                    Client.RemoveClient(clients);
                    break;
                case 7:
                    ExibirTituloDaOpcao("Mostrar livros");
                    Book.ShowBooks(catalog);
                    HideThrash();
                    break;
                case 8:
                    ExibirTituloDaOpcao("Mostrar clientes");
                    Client.ShowClients(clients);
                    break;
                case 9:
                    ExibirTituloDaOpcao("Livros alugados pelo cliente");
                    Client.ShowClientRentals(clients);
                    break;
                default:
                    Console.WriteLine("escolha uma opção válida. Reiniciando...");
                    Thread.Sleep(3000);
                    break;
            }
        } while (op != 0);

        FileLoader.SerializeList(bookPath, catalog);
        FileLoader.SerializeList(clientPath, clients);
    }

    public static void ExibirTituloDaOpcao(string titulo)
    {
        Console.Clear();
        int quantidadeDeLetras = titulo.Length;
        string asteriscos = string.Empty.PadLeft(quantidadeDeLetras, '*');
        Console.WriteLine(asteriscos);
        Console.WriteLine(titulo);
        Console.WriteLine(asteriscos + "\n");

    }

    public static void SucessMessage(string message)
    {
        Console.WriteLine(message);
        Thread.Sleep(3000);
    }

    /*unica maneira que achei de esconder o lixo, ja que o console.clear so apagar a parte visivel das listas que gero com foreach,
    nenhuma das minhas tentativas para apagar completamente o buffer tiveram sucesso*/
    public static void HideThrash()
    {
        Console.WriteLine("lixo que o buffer não consegue apagar acima");
        for (int i = 0; i < 100; i++)
        {
            Console.WriteLine(" ");
        }
    }


}
