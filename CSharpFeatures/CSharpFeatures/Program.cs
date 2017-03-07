using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;

namespace CSharpFeatures
{
	sealed class User
	{
		public string Name { get; }
		public int Age { get; }
		public bool IsNew { get; }

		private User(string name, int age, bool isNew)
		{
			Name = name;
			Age = age;
			IsNew = isNew;
		}

		public static List<User> GetUsers()
		{
			return new List<User>() {
				new User("Juan", 10, true),
				new User("María", 11, false),
				new User("John", 12, false),
				new User("Mary", 13, false),
				new User("José", 14, true),
				new User("Lucía", 15, false),
				new User("Joseph", 16, true),
				new User("Lucy", 17, false),
				new User("Pedro", 18, true),
				new User("Erika", 19, false),
				new User("Lel", 19, true)
			};
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			Collections();
			AnonymousObjects();
			Linq();
			Console.Read();
		}

		/// <summary>
		/// Listas y diccionarios.
		/// </summary>
		private static void Collections()
		{
			var list = new List<int>() { 1, 2, 3, 4, 5 };
			var dict = new Dictionary<string, int>() {
				{ "name", 1 },
				{ "age", 2 },
				{ "isNew", 3 }

			};

			// Enumeración de las colecciones.
			Console.WriteLine("--- Método Collections() ---\n");

			list.ForEach(e => Console.WriteLine(e));
			foreach (var entry in dict)
				Console.WriteLine("Llave: {0} || Valor: {1}", entry.Key, entry.Value);

			Console.WriteLine("\n--- Termina Método Collections() ---\n");
		}

		/// <summary>
		/// Objetos anónimos comunes y anidados.
		/// </summary>
		private static void AnonymousObjects()
		{
			Console.WriteLine("--- Método AnonymousObjects() ---\n");

			// Objeto anónimo simple
			var obj = new {
				Name = "Pepe",
				Age = 23,
				IsNew = true
			};

			Console.WriteLine("Name: {0} | Age: {1} | Is new? {2}\n", obj.Name, obj.Age, obj.IsNew);

			// Objetos anónimos anidados
			// Lista de objetos anónimos.
			var complexObj = new {
				ID = 1,
				Attributes = new List<dynamic>() {
					new {
						Name = "color",
						Value = 255
					},
					new {
						Name = "position",
						X = 23.4,
						Y = 11.1
					}
				}
			};

			Console.WriteLine("Id del objeto anidado: {0}", complexObj.ID);
			complexObj.Attributes.ForEach(e => {
				Console.WriteLine("Objeto anónimo:\n--------------");

				foreach (var descriptor in TypeDescriptor.GetProperties(e))
					Console.WriteLine("{0} = {1}", descriptor.Name, descriptor.GetValue(e));

				Console.WriteLine();
			});

			Console.WriteLine("Value = {0} || X = {1} || Y = {2}", complexObj.Attributes[0].Value, complexObj.Attributes[1].X, complexObj.Attributes[1].Y);

			// Filtro con un List<dynamic>
			var filtered = complexObj.Attributes.Where(e => e.Name == "color").ToList<dynamic>();
			Console.WriteLine("Objeto filtrado: Value = {0}", filtered[0].Value);

			Console.WriteLine("\n--- Termina Método AnonymousObjects() ---\n");
		}

		/// <summary>
		/// Consultas básicas con LINQ
		/// </summary>
		private static void Linq()
		{
			Console.WriteLine("--- Método Linq() ---\n");

			var users          = User.GetUsers();
			var trueUsersNames = from u in users where u.IsNew orderby u.Name ascending select u.Name;
			var oldUsers       = from u in users where u.Age >= 18 select new { u.Name, u.Age };
			var johns          = from u in users where u.Name == "John" || u.Name == "Juan" select new { u.Age, u.IsNew };

			trueUsersNames.ToList().ForEach(u => Console.WriteLine("Usuario nuevo: {0}.", u));
			Console.WriteLine();

			oldUsers.ToList().ForEach(u => Console.WriteLine("Usuario viejo '{0}' de {1} años de edad.", u.Name, u.Age));
			Console.WriteLine();

			johns.ToList().ForEach(u => Console.WriteLine("John|Juan tiene {0} años y {1}", u.Age, (u.IsNew ? "es nuevo." : "ya existe.")));

			Console.WriteLine("\n--- Termina Método Linq() ---\n");
		}
	}
}