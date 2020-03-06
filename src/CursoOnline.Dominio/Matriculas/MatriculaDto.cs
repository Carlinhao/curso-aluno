namespace CurosOnline.Dominio.Matriculas
{
    public class MatriculaDto
    {
        public MatriculaDto(int alunoId, int cursoId)
        {
            AlunoId = alunoId;
            CursoId = cursoId;
        }

        public int AlunoId { get; set; }
        public int CursoId { get; set; }
        public double ValorPago { get; set; }
    }
}
