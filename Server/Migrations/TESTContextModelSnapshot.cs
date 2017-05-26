using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Server.Models;

namespace WebApp.Migrations
{
    [DbContext(typeof(TESTContext))]
    partial class TESTContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OpenIddict.Models.OpenIddictApplication", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientId");

                    b.Property<string>("ClientSecret");

                    b.Property<string>("DisplayName");

                    b.Property<string>("LogoutRedirectUri");

                    b.Property<string>("RedirectUri");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ClientId")
                        .IsUnique();

                    b.ToTable("OpenIddictApplications");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictAuthorization", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationId");

                    b.Property<string>("Scope");

                    b.Property<string>("Subject");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("OpenIddictAuthorizations");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictScope", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("OpenIddictScopes");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictToken", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationId");

                    b.Property<string>("AuthorizationId");

                    b.Property<string>("Subject");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("AuthorizationId");

                    b.ToTable("OpenIddictTokens");
                });

            modelBuilder.Entity("WebApp.Models.Genre", b =>
                {
                    b.Property<int>("GenreId");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("StatusId");

                    b.HasKey("GenreId");

                    b.HasIndex("StatusId");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("WebApp.Models.Person", b =>
                {
                    b.Property<int>("PersonId");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("StatusId");

                    b.HasKey("PersonId");

                    b.HasIndex("StatusId");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("WebApp.Models.PersonDetails", b =>
                {
                    b.Property<int>("PersonId");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("GenreId");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("PersonId")
                        .HasName("PK_PersonDetails");

                    b.HasIndex("GenreId");

                    b.ToTable("PersonDetails");
                });

            modelBuilder.Entity("WebApp.Models.Status", b =>
                {
                    b.Property<int>("StatusId");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("StatusId");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictAuthorization", b =>
                {
                    b.HasOne("OpenIddict.Models.OpenIddictApplication", "Application")
                        .WithMany("Authorizations")
                        .HasForeignKey("ApplicationId");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictToken", b =>
                {
                    b.HasOne("OpenIddict.Models.OpenIddictApplication", "Application")
                        .WithMany("Tokens")
                        .HasForeignKey("ApplicationId");

                    b.HasOne("OpenIddict.Models.OpenIddictAuthorization", "Authorization")
                        .WithMany("Tokens")
                        .HasForeignKey("AuthorizationId");
                });

            modelBuilder.Entity("WebApp.Models.Genre", b =>
                {
                    b.HasOne("WebApp.Models.Status", "Status")
                        .WithMany("Genre")
                        .HasForeignKey("StatusId")
                        .HasConstraintName("FK_Genre_Status");
                });

            modelBuilder.Entity("WebApp.Models.Person", b =>
                {
                    b.HasOne("WebApp.Models.Status", "Status")
                        .WithMany("Person")
                        .HasForeignKey("StatusId")
                        .HasConstraintName("FK_Person_Status");
                });

            modelBuilder.Entity("WebApp.Models.PersonDetails", b =>
                {
                    b.HasOne("WebApp.Models.Genre", "Genre")
                        .WithMany("PersonDetails")
                        .HasForeignKey("GenreId")
                        .HasConstraintName("FK_PersonDetails_Genre");

                    b.HasOne("WebApp.Models.Person", "Person")
                        .WithOne("PersonDetails")
                        .HasForeignKey("WebApp.Models.PersonDetails", "PersonId")
                        .HasConstraintName("FK_PersonDetails_Person");
                });
        }
    }
}
