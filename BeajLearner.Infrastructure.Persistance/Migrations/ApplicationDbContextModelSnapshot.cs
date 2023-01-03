﻿// <auto-generated />
using System;
using BeajLearner.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BeajLearner.Infrastructure.Persistance.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BeajLearner.Domain.Entities.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CourseId"));

                    b.Property<int>("CourseCategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("CourseName")
                        .HasColumnType("text");

                    b.Property<int?>("CoursePrice")
                        .HasColumnType("integer");

                    b.Property<int?>("CourseWeeks")
                        .HasColumnType("integer");

                    b.Property<string>("status")
                        .HasColumnType("text");

                    b.Property<bool>("subscribed")
                        .HasColumnType("boolean");

                    b.HasKey("CourseId");

                    b.HasIndex("CourseCategoryId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("BeajLearner.Domain.Entities.CourseCategory", b =>
                {
                    b.Property<int>("CourseCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CourseCategoryId"));

                    b.Property<string>("CourseCategoryName")
                        .HasColumnType("text");

                    b.Property<string>("image")
                        .HasColumnType("text");

                    b.HasKey("CourseCategoryId");

                    b.ToTable("CourseCategories");
                });

            modelBuilder.Entity("BeajLearner.Domain.Entities.CourseWeek", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int>("courseId")
                        .HasColumnType("integer");

                    b.Property<string>("description")
                        .HasColumnType("text");

                    b.Property<string>("image")
                        .HasColumnType("text");

                    b.Property<int>("weekNumber")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.ToTable("courseWeek");
                });

            modelBuilder.Entity("BeajLearner.Domain.Entities.Lesson", b =>
                {
                    b.Property<int>("LessonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("LessonId"));

                    b.Property<string[]>("Audios")
                        .HasColumnType("text[]");

                    b.Property<string>("activity")
                        .HasColumnType("text");

                    b.Property<string>("activityAlias")
                        .HasColumnType("text");

                    b.Property<int>("courseId")
                        .HasColumnType("integer");

                    b.Property<int?>("dayNumber")
                        .HasColumnType("integer");

                    b.Property<string[]>("image")
                        .HasColumnType("text[]");

                    b.Property<string>("lessonType")
                        .HasColumnType("text");

                    b.Property<string>("text")
                        .HasColumnType("text");

                    b.Property<string[]>("videos")
                        .HasColumnType("text[]");

                    b.Property<int?>("weekNumber")
                        .HasColumnType("integer");

                    b.HasKey("LessonId");

                    b.HasIndex("courseId");

                    b.ToTable("Lesson");
                });

            modelBuilder.Entity("BeajLearner.Domain.Entities.mcqQuestions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("correctAnswer")
                        .HasColumnType("text");

                    b.Property<int>("lessonId")
                        .HasColumnType("integer");

                    b.Property<string>("option1")
                        .HasColumnType("text");

                    b.Property<string>("option2")
                        .HasColumnType("text");

                    b.Property<string>("option3")
                        .HasColumnType("text");

                    b.Property<string>("option4")
                        .HasColumnType("text");

                    b.Property<string>("question")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("lessonId");

                    b.ToTable("mcqQuestions");
                });

            modelBuilder.Entity("BeajLearner.Domain.Entities.Otp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("StartTime")
                        .HasColumnType("text");

                    b.Property<int>("otp")
                        .HasColumnType("integer");

                    b.Property<string>("recieverNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Otps");
                });

            modelBuilder.Entity("BeajLearner.Domain.Entities.purchasedCourse", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int>("courseId")
                        .HasColumnType("integer");

                    b.Property<string>("customerId")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("purchasedCourses");
                });

            modelBuilder.Entity("BeajLearner.Domain.Entities.Teacher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("BeajLearner.Domain.Entities.TeachersAssignedCourse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("courseId")
                        .HasColumnType("integer");

                    b.Property<string>("teacherId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TeachersAssignedCourses");
                });

            modelBuilder.Entity("BeajLearner.Domain.Entities.Course", b =>
                {
                    b.HasOne("BeajLearner.Domain.Entities.CourseCategory", "CourseCategory")
                        .WithMany("Course")
                        .HasForeignKey("CourseCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourseCategory");
                });

            modelBuilder.Entity("BeajLearner.Domain.Entities.Lesson", b =>
                {
                    b.HasOne("BeajLearner.Domain.Entities.Course", "Course")
                        .WithMany("lessons")
                        .HasForeignKey("courseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("BeajLearner.Domain.Entities.mcqQuestions", b =>
                {
                    b.HasOne("BeajLearner.Domain.Entities.Lesson", null)
                        .WithMany("mcqquestion")
                        .HasForeignKey("lessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BeajLearner.Domain.Entities.Course", b =>
                {
                    b.Navigation("lessons");
                });

            modelBuilder.Entity("BeajLearner.Domain.Entities.CourseCategory", b =>
                {
                    b.Navigation("Course");
                });

            modelBuilder.Entity("BeajLearner.Domain.Entities.Lesson", b =>
                {
                    b.Navigation("mcqquestion");
                });
#pragma warning restore 612, 618
        }
    }
}
