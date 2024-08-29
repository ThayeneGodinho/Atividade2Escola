using MySql.Data.MySqlClient;

namespace appEscola
{
    public class alunos
    {
        private static string connectionString =
            "Server=localhost;Port=3306;Database=db_aulas_2024;User=kaique;password=1234567;SslMode=none;";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1 - Cadastrar Aluno");
                Console.WriteLine("2 - Listar Aluno");
                Console.WriteLine("3 - Editar Aluno");
                Console.WriteLine("4 - Excluir Aluno");
                Console.WriteLine("5 - Sair");
                Console.Write("Escolha uma opção acima: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        CadastrarAluno();
                        break;
                    case "2":
                        ListarAluno();
                        break;
                    case "3":
                        Editar();
                        break;
                    case "4":
                        Excluir();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Opção inválida");
                        break;
                }
            }
        }
        static void CadastrarAluno()
        {
            Console.Write("Informe o Nome do aluno: ");
            string nome = Console.ReadLine();

            Console.Write("Informe a idade do aluno: ");
            int idade = int.Parse(Console.ReadLine());

            Console.Write("Informe o curso: ");
            string curso = Console.ReadLine();

            Console.Write("Informe a data de matrícula do aluno: ");
            DateTime datamatricula = DateTime.Parse(Console.ReadLine());

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO alunos (nome,idade,curso,datamatricula) VALUES (@nome,@idade,@curso, @datamatricula)";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@idade", idade);
                cmd.Parameters.AddWithValue("@curso", curso);
                cmd.Parameters.AddWithValue("@datamatricula", datamatricula);
             
                cmd.ExecuteNonQuery();
            }

            Console.WriteLine("Aluno cadastrado com sucesso");
        }
        static void ListarAluno()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id, nome, idade, curso, datamatricula FROM alunos";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Id: {reader["Id"]}, nome: {reader["nome"]}, idade: {reader["idade"]},curso: {reader["curso"]}, datamatricula: {reader["datamatricula"]}");

                        }
                    }
                    else
                    {
                        Console.WriteLine("Não existe aluno cadastrado");
                    }

                }

            }
        }
        static void Excluir()
        {
            Console.Write("Informe o Id do aluno que deseja excluir: ");
            int idExclusao = int.Parse(Console.ReadLine());

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM alunos WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@Id", idExclusao);

                int linhaAfetada = cmd.ExecuteNonQuery();
                if (linhaAfetada > 0)
                {
                    Console.WriteLine("Aluno excluido com sucesso");
                }
                else
                {
                    Console.WriteLine("Aluno não encontrado");
                }
            }
        }
        static void Editar()
        {
            Console.Write("Informe o Id do aluno que deseja editar: ");
            int idEditar = int.Parse(Console.ReadLine());

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM alunos WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", idEditar);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.Write("Informe o novo nome: (* Deixe o campo em branco, para não alterar): ");
                        string novonome = Console.ReadLine();

                        Console.Write("Informe a nova idade: (* Deixe o campo em branco, para não alterar): ");
                       string novaidade = Console.ReadLine();

                        Console.Write("Informe o novo curso: (* Deixe o campo em branco, para não alterar): ");
                        string novocurso = Console.ReadLine();


                        Console.Write("Informe a nova data de matricula : (* Deixe o campo em branco, para não alterar): ");
                      string novadatamatriucla =  Console.ReadLine();

                        reader.Close();

                        string queryUpdate = "UPDATE alunos SET nome= @nome, idade = @idade, curso= @curso, datamatricula = @datamatricula  WHERE Id = @Id";
                        cmd = new MySqlCommand(queryUpdate, connection);
                        cmd.Parameters.AddWithValue("@nome", string.IsNullOrWhiteSpace(novonome) ? reader["nome"] : novonome);
                        cmd.Parameters.AddWithValue("@idade", string.IsNullOrWhiteSpace(novaidade) ? reader["idade"] : int.Parse(novaidade));
                        cmd.Parameters.AddWithValue("@curso", string.IsNullOrWhiteSpace(novocurso) ? reader["curso"] : novocurso);
                        cmd.Parameters.AddWithValue("@datamatricula", string.IsNullOrWhiteSpace(novadatamatriucla) ? reader["datamatricula"] : DateTime.Parse( novadatamatriucla));
                        cmd.Parameters.AddWithValue("@Id", idEditar);

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("O aluno foi atualizado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("O Id do aluno informado não existe!");
                    }
                }
            }

        }
    }
}