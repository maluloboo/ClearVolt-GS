﻿// <auto-generated />
using System;
using ClearVolt.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace ClearVolt.Migrations
{
    [DbContext(typeof(ClearVoltDbContext))]
    [Migration("20241121174524_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ClearVolt.Domain.Models.ConfiguracaoColetaModel", b =>
                {
                    b.Property<int>("id_configuracao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_configuracao"));

                    b.Property<string>("descricao")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("id_dispositivo")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("intervalo_de_horas")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("temp_max")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("tempo_de_umidade_min")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("umidade_min")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("id_configuracao");

                    b.HasIndex("id_dispositivo");

                    b.ToTable("Configuracao_Coleta");
                });

            modelBuilder.Entity("ClearVolt.Domain.Models.DadoColetadoModel", b =>
                {
                    b.Property<int>("id_dado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_dado"));

                    b.Property<DateTime>("data_dado")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<int>("id_dispositivo")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("identificador")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("temperatura")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("umidade")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("id_dado");

                    b.HasIndex("id_dispositivo");

                    b.ToTable("Dado_Coletado");
                });

            modelBuilder.Entity("ClearVolt.Domain.Models.DispositivoModel", b =>
                {
                    b.Property<int>("id_dispositivo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_dispositivo"));

                    b.Property<int>("id_usuario")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("identificador")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("marca")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("id_dispositivo");

                    b.HasIndex("id_usuario");

                    b.ToTable("Dispositivo");
                });

            modelBuilder.Entity("ClearVolt.Domain.Models.PessoaModel", b =>
                {
                    b.Property<int>("id_pessoa")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_pessoa"));

                    b.Property<string>("cpf")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<DateTime>("data_nascimento")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<int>("id_usuario")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("sobrenome")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("telefone")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("id_pessoa");

                    b.HasIndex("id_usuario");

                    b.ToTable("Pessoa");
                });

            modelBuilder.Entity("ClearVolt.Domain.Models.RoleModel", b =>
                {
                    b.Property<int>("id_role")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_role"));

                    b.Property<int>("id_usuario")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("id_role");

                    b.HasIndex("id_usuario");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("ClearVolt.Domain.Models.UsuarioModel", b =>
                {
                    b.Property<int>("id_usuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_usuario"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("senha")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("id_usuario");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("ClearVolt.Domain.Models.ConfiguracaoColetaModel", b =>
                {
                    b.HasOne("ClearVolt.Domain.Models.DispositivoModel", "Dispositivo")
                        .WithMany("ConfigColeta")
                        .HasForeignKey("id_dispositivo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dispositivo");
                });

            modelBuilder.Entity("ClearVolt.Domain.Models.DadoColetadoModel", b =>
                {
                    b.HasOne("ClearVolt.Domain.Models.DispositivoModel", "Dispositivo")
                        .WithMany("DadoColetado")
                        .HasForeignKey("id_dispositivo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dispositivo");
                });

            modelBuilder.Entity("ClearVolt.Domain.Models.DispositivoModel", b =>
                {
                    b.HasOne("ClearVolt.Domain.Models.UsuarioModel", "Usuario")
                        .WithMany("Dispositivos")
                        .HasForeignKey("id_usuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ClearVolt.Domain.Models.PessoaModel", b =>
                {
                    b.HasOne("ClearVolt.Domain.Models.UsuarioModel", "Usuario")
                        .WithMany("Pessoas")
                        .HasForeignKey("id_usuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ClearVolt.Domain.Models.RoleModel", b =>
                {
                    b.HasOne("ClearVolt.Domain.Models.UsuarioModel", "Usuario")
                        .WithMany("Roles")
                        .HasForeignKey("id_usuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ClearVolt.Domain.Models.DispositivoModel", b =>
                {
                    b.Navigation("ConfigColeta");

                    b.Navigation("DadoColetado");
                });

            modelBuilder.Entity("ClearVolt.Domain.Models.UsuarioModel", b =>
                {
                    b.Navigation("Dispositivos");

                    b.Navigation("Pessoas");

                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}