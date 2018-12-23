﻿// <auto-generated />
using System;
using GrammarWorkbook.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GrammarWorkbook.Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GrammarWorkbook.Data.Models.Dialog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("DialogExerciseId");

                    b.Property<Guid?>("SentenceId");

                    b.HasKey("Id");

                    b.HasIndex("DialogExerciseId");

                    b.HasIndex("SentenceId");

                    b.ToTable("Dialog");
                });

            modelBuilder.Entity("GrammarWorkbook.Data.Models.Exercise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Title");

                    b.Property<Guid>("TopicId");

                    b.Property<bool>("UseOptions");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.ToTable("Exercises");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Exercise");
                });

            modelBuilder.Entity("GrammarWorkbook.Data.Models.Sentence", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("FillTheBlanksExerciseId");

                    b.Property<bool>("IsFullWidth");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("FillTheBlanksExerciseId");

                    b.ToTable("Sentences");
                });

            modelBuilder.Entity("GrammarWorkbook.Data.Models.Topic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("SubTitle");

                    b.Property<string>("Title");

                    b.Property<Guid>("UnitId");

                    b.HasKey("Id");

                    b.HasIndex("UnitId");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("GrammarWorkbook.Data.Models.Unit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("GrammarWorkbook.Data.Models.DialogExercise", b =>
                {
                    b.HasBaseType("GrammarWorkbook.Data.Models.Exercise");

                    b.HasDiscriminator().HasValue("dialogue");
                });

            modelBuilder.Entity("GrammarWorkbook.Data.Models.FillTheBlanksExercise", b =>
                {
                    b.HasBaseType("GrammarWorkbook.Data.Models.Exercise");

                    b.HasDiscriminator().HasValue("fill");
                });

            modelBuilder.Entity("GrammarWorkbook.Data.Models.MatchTheWordsExercise", b =>
                {
                    b.HasBaseType("GrammarWorkbook.Data.Models.Exercise");

                    b.HasDiscriminator().HasValue("match");
                });

            modelBuilder.Entity("GrammarWorkbook.Data.Models.Dialog", b =>
                {
                    b.HasOne("GrammarWorkbook.Data.Models.DialogExercise")
                        .WithMany("Dialogs")
                        .HasForeignKey("DialogExerciseId");

                    b.HasOne("GrammarWorkbook.Data.Models.Sentence", "Sentence")
                        .WithMany()
                        .HasForeignKey("SentenceId");
                });

            modelBuilder.Entity("GrammarWorkbook.Data.Models.Exercise", b =>
                {
                    b.HasOne("GrammarWorkbook.Data.Models.Topic", "Topic")
                        .WithMany("Exercises")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GrammarWorkbook.Data.Models.Sentence", b =>
                {
                    b.HasOne("GrammarWorkbook.Data.Models.FillTheBlanksExercise")
                        .WithMany("Sentences")
                        .HasForeignKey("FillTheBlanksExerciseId");
                });

            modelBuilder.Entity("GrammarWorkbook.Data.Models.Topic", b =>
                {
                    b.HasOne("GrammarWorkbook.Data.Models.Unit", "Unit")
                        .WithMany("Topics")
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
