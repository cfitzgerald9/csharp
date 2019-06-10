﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentExercises_Entity.Data;

namespace StudentExercises_Entity.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190610135201_create-tables")]
    partial class createtables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StudentExercises_Entity.Models.Cohort", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Cohort");
                });

            modelBuilder.Entity("StudentExercises_Entity.Models.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Language")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Exercise");
                });

            modelBuilder.Entity("StudentExercises_Entity.Models.Instructor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CohortId");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("SlackHandle")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CohortId");

                    b.ToTable("Instructor");
                });

            modelBuilder.Entity("StudentExercises_Entity.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CohortId");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("SlackHandle")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CohortId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("StudentExercises_Entity.Models.StudentExercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExerciseId");

                    b.Property<int>("StudentId");

                    b.Property<bool>("isComplete");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentExercise");
                });

            modelBuilder.Entity("StudentExercises_Entity.Models.Instructor", b =>
                {
                    b.HasOne("StudentExercises_Entity.Models.Cohort", "Cohort")
                        .WithMany("Instructors")
                        .HasForeignKey("CohortId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StudentExercises_Entity.Models.Student", b =>
                {
                    b.HasOne("StudentExercises_Entity.Models.Cohort", "Cohort")
                        .WithMany("Students")
                        .HasForeignKey("CohortId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StudentExercises_Entity.Models.StudentExercise", b =>
                {
                    b.HasOne("StudentExercises_Entity.Models.Exercise", "Exercise")
                        .WithMany("StudentExercises")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StudentExercises_Entity.Models.Student", "Student")
                        .WithMany("StudentExercises")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
