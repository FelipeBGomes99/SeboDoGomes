using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace SeboDoGomesV2.Models
{
    internal class Book
    {
        [JsonPropertyName("Name")]
        public string? Name { get;  private set; }

        [JsonPropertyName("Writer")]
        public Writer? Writer { get; private set; }

        [JsonPropertyName("ReleaseDate")]
        public DateTime ReleaseDate { get; private set; }

        [JsonPropertyName("Pages")]
        public int Pages { get; private set; }

        [JsonPropertyName("Publisher")]
        public string? Publisher { get; private set; }

        [JsonPropertyName("IsAvailable")]
        public bool IsAvailable { get; private set; }

        public Book() { }

        [JsonConstructor]
        public Book(string Name, DateTime ReleaseDate, Writer Writer, int Pages, string Publisher, bool IsAvailable)
        {
            this.Name = Name;
            this.Writer = Writer ?? new Writer("Desconhecido", "N/A", new DateTime(0001, 01, 01));
            this.ReleaseDate = ReleaseDate;
            this.Pages = Pages;
            this.Publisher = Publisher;
            this.IsAvailable = IsAvailable;
        }

        public override string ToString()
        {
            return $"[Book: {Name}\n" +
                   $"Release Date: {ReleaseDate.ToShortDateString()}\n" +
                   "Writer: " + Writer!.ToString() +
                   $", Pages: {Pages}\n" +
                   $"Publisher: {Publisher}]\n" +
                   $", Is Available: {IsAvailable}\n";
        }

        public static void RegisterBook(List<Book> catalog)
        {
            Console.WriteLine("Nome do autor: ");
            string writerName = Console.ReadLine()!;
            Console.WriteLine("Nascimento do autor(dd/MM/yyyy):");
            DateTime writerBirth = DateTime.Parse(Console.ReadLine()!);
            Console.WriteLine("Nacionalidade do autor: ");
            string writerNation = Console.ReadLine()!;

            Writer bookWriter = new Writer(writerName, writerNation, writerBirth);

            Console.WriteLine("Nome do livro: ");
            string bookName = Console.ReadLine()!;
            Console.WriteLine("Data de lançamento(dd/MM/yyyy): ");
            DateTime bookRelease = DateTime.Parse(Console.ReadLine()!);
            Console.WriteLine("Paginas: ");
            int bookPages = int.Parse(Console.ReadLine()!);
            Console.WriteLine("Editora; ");
            string bookPublisher = Console.ReadLine()!;

            bool IsAvailable = true;

            catalog.Add(new Book(bookName, bookRelease, bookWriter, bookPages, bookPublisher, IsAvailable));
        }

        public static void ShowBooks(List<Book> catalog)
        {
            foreach (var item in catalog)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine($"qtd de livros:{catalog.Count}");
            
            Console.ReadKey();
        }

        public static void RemoveBook(List<Book> books)
        {
            
            Console.WriteLine("Nome do livro que deseja excluir: ");
            string excludedName = Console.ReadLine()!;

            Book removed = books.FirstOrDefault(book => book.Name!.Equals(excludedName, StringComparison.OrdinalIgnoreCase))!;

            books.Remove(removed);

            Console.WriteLine($"\n{removed} ---- REMOVIDO");

            Console.ReadKey();
        }

        public static void BookRental(List<Book> books, List<Client> clients)
        {
            Console.WriteLine("Nome do cliente: ");
            string clientName = Console.ReadLine()!;
            Client rentalClient = clients.FirstOrDefault(c => c.Name!.Equals(clientName, StringComparison.OrdinalIgnoreCase));

            if (rentalClient == null)
            {
                Console.WriteLine("Cliente não encontrado, criando um novo cliente.");
                Client.RegisterClient(clients);
                rentalClient = clients.Last(); 
            }

            Console.WriteLine("nome do livro a ser alugado");
            string bookName = Console.ReadLine()!;
            Book rentalBook = books.FirstOrDefault(b => b.Name!.Equals(bookName, StringComparison.OrdinalIgnoreCase) && b.IsAvailable);

            if (rentalBook != null)
            {
                rentalBook.IsAvailable = false;
                rentalClient.Rentals.Add(rentalBook);
                Console.WriteLine($"Livro '{rentalBook.Name}' alugado com sucesso para {rentalClient.Name}.");
            }
            else
            {
                Console.WriteLine("Livro não disponível ou não encontrado.");
            }
            Thread.Sleep(3000);
        }

        public static void ReturnBook(List<Book> books, List<Client> clients) 
        {

            Console.WriteLine("nome do livro a ser devolvido:");
            string name = Console.ReadLine()!;
            Console.WriteLine("nome do cliente: ");
            string clientName = Console.ReadLine()!;

            
            Client clientReturn = clients.FirstOrDefault(client => client.Name!.Equals(clientName, StringComparison.OrdinalIgnoreCase));

            if (clientReturn == null)
            {
                Console.WriteLine("Cliente não encontrado.");
                return;
            }

            Book returnBook = clientReturn.Rentals.FirstOrDefault(c => c.Name!.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (returnBook == null)
            {
                Console.WriteLine("Livro não encontrado no acervo.");
                return;
            }

            if (!clientReturn.Rentals.Contains(returnBook))
            {
                Console.WriteLine("Este cliente não alugou este livro.");
                return;
            }

            Book mainBookReference = books.FirstOrDefault(b => b.Name!.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (mainBookReference == null)
            {
                Console.WriteLine("Erro: O livro não está cadastrado na biblioteca.");
                return;
            }

            mainBookReference.IsAvailable = true;
            clientReturn.Rentals.Remove(returnBook);

            Console.WriteLine($"{name} disponivel novamente!!");
            Console.ReadKey();

        }

    }
}
