namespace CurosOnline.Dominio.Matriculas
{
    public class MatriculaDto
    {
        public MatriculaDto(int alunoId, int cursoId, decimal valorPago)
        {
            AlunoId = alunoId;
            CursoId = cursoId;
            ValorPago = valorPago;
        }

        public int AlunoId { get; set; }
        public int CursoId { get; set; }
        public decimal ValorPago { get; set; }
    }
}
