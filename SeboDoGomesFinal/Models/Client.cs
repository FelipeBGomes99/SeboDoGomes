using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SeboDoGomesV2.Models
{
    internal class Client
    {
        [JsonPropertyName("Name")]
        public string Name { get; private set; }

        [JsonPropertyName("Cpf")]
        public string Cpf { get; private set; }

        [JsonPropertyName("Rentals")]
        public List<Book> Rentals { get; private set; }


        [JsonConstructor]
        public Client(string Name, string Cpf, List<Book> Rentals) {
            this.Name = Name;
            this.Cpf = Cpf;
            this.Rentals = Rentals;
        }

        public Client() { }

        public override string ToString()
        {
            return $"[Nome: {Name}, " + $"CPF: {Cpf}";
        }

        public static void RegisterClient(List<Client> clients)
        {
            Console.WriteLine("Nome do cliente: ");
            string clientName = Console.ReadLine()!;
            Console.WriteLine("CPF do cliente: ");
            string clientCPF = Console.ReadLine()!;

            clients.Add(new Client(clientName, clientCPF, new List<Book>()));

        }

        public static void ShowClients(List<Client> clients)
        {
            foreach (var client in clients)
            {
                Console.WriteLine(client);
            }

            Console.ReadKey();

        }

        public static void RemoveClient(List<Client> clients)
        {
            ShowClients(clients);
            Console.WriteLine("Nome do cliente que deseja excluir: ");
            string excludedName = Console.ReadLine()!;

            Client removed = clients.FirstOrDefault(client => client.Name!.Equals(excludedName, StringComparison.OrdinalIgnoreCase))!;

            clients.Remove(removed);

            Console.WriteLine($"\n{removed} ---- REMOVIDO");

            Console.ReadKey();
        }

        public static void ShowClientRentals(List<Client> clients)
        {
            Console.WriteLine("nome do cliente: ");
            string name = Console.ReadLine()!;

            Client client = clients.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase))!;

            client.Rentals.ForEach(book => Console.WriteLine(book));

            Console.ReadKey();
        }

    }
}
