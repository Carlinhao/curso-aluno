using Microsoft.EntityFrameworkCore.Migrations;

namespace CursoOnline.Infrastructure.Migrations
{
    public partial class CriacaoTabelaMatricula : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "Cursos",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.CreateTable(
                name: "Matriculas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlunoId = table.Column<int>(nullable: true),
                    CursoId = table.Column<int>(nullable: true),
                    ValorPago = table.Column<decimal>(nullable: false),
                    PossuiDesconto = table.Column<bool>(nullable: false),
                    NotaDoAluno = table.Column<double>(nullable: false),
                    CursoConcluido = table.Column<bool>(nullable: false),
                    Cancelada = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matriculas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matriculas_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matriculas_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_AlunoId",
                table: "Matriculas",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_CursoId",
                table: "Matriculas",
                column: "CursoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matriculas");

            migrationBuilder.AlterColumn<double>(
                name: "Valor",
                table: "Cursos",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
