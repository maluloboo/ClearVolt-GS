using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClearVolt.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    senha = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "Dispositivo",
                columns: table => new
                {
                    id_dispositivo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    marca = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    identificador = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    id_usuario = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dispositivo", x => x.id_dispositivo);
                    table.ForeignKey(
                        name: "FK_Dispositivo_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    id_pessoa = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    sobrenome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    data_nascimento = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    cpf = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    telefone = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    id_usuario = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.id_pessoa);
                    table.ForeignKey(
                        name: "FK_Pessoa_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    id_role = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    id_usuario = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.id_role);
                    table.ForeignKey(
                        name: "FK_Role_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Configuracao_Coleta",
                columns: table => new
                {
                    id_configuracao = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    temp_max = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    umidade_min = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    tempo_de_umidade_min = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    intervalo_de_horas = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    id_dispositivo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuracao_Coleta", x => x.id_configuracao);
                    table.ForeignKey(
                        name: "FK_Configuracao_Coleta_Dispositivo_id_dispositivo",
                        column: x => x.id_dispositivo,
                        principalTable: "Dispositivo",
                        principalColumn: "id_dispositivo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dado_Coletado",
                columns: table => new
                {
                    id_dado = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    temperatura = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    umidade = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    data_dado = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    identificador = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    id_dispositivo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dado_Coletado", x => x.id_dado);
                    table.ForeignKey(
                        name: "FK_Dado_Coletado_Dispositivo_id_dispositivo",
                        column: x => x.id_dispositivo,
                        principalTable: "Dispositivo",
                        principalColumn: "id_dispositivo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Configuracao_Coleta_id_dispositivo",
                table: "Configuracao_Coleta",
                column: "id_dispositivo");

            migrationBuilder.CreateIndex(
                name: "IX_Dado_Coletado_id_dispositivo",
                table: "Dado_Coletado",
                column: "id_dispositivo");

            migrationBuilder.CreateIndex(
                name: "IX_Dispositivo_id_usuario",
                table: "Dispositivo",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_id_usuario",
                table: "Pessoa",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Role_id_usuario",
                table: "Role",
                column: "id_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configuracao_Coleta");

            migrationBuilder.DropTable(
                name: "Dado_Coletado");

            migrationBuilder.DropTable(
                name: "Pessoa");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Dispositivo");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
