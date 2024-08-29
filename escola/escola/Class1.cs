namespace appEscola
{
    public class Alunos
    {
        public int id { get; set; }
        public string nome { get; set; }

        public int idade { get; set; }

        public string curso { get; set; }

        public DateTime datamatricula { get; set; }

  

        public override string ToString()
        {
            return $"ID: {id}, nome: {nome}, idade: {idade}, curso: {curso}, data matricula: {datamatricula}";
        }

    }
}
