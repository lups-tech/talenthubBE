﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace talenthubBE.Migrations
{
    [DbContext(typeof(MvcDataContext))]
    [Migration("20231031111624_AmendedCommentColumns")]
    partial class AmendedCommentColumns
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Developer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("OrganizationId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Developers");
                });

            modelBuilder.Entity("DeveloperSkill", b =>
                {
                    b.Property<Guid>("DevelopersId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SkillsId")
                        .HasColumnType("uuid");

                    b.HasKey("DevelopersId", "SkillsId");

                    b.HasIndex("SkillsId");

                    b.ToTable("DeveloperSkill");
                });

            modelBuilder.Entity("DeveloperUser", b =>
                {
                    b.Property<Guid>("DevelopersId")
                        .HasColumnType("uuid");

                    b.Property<string>("UsersId")
                        .HasColumnType("text");

                    b.HasKey("DevelopersId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("DeveloperUser");
                });

            modelBuilder.Entity("Job", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Deadline")
                        .HasColumnType("text")
                        .HasColumnName("deadline");

                    b.Property<string>("Employer")
                        .HasColumnType("text")
                        .HasColumnName("employer");

                    b.Property<string>("JobTechId")
                        .HasColumnType("text")
                        .HasColumnName("jobTech_id");

                    b.Property<string>("JobText")
                        .HasColumnType("text")
                        .HasColumnName("job_text");

                    b.Property<string>("Title")
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<string>("Url")
                        .HasColumnType("text")
                        .HasColumnName("url");

                    b.HasKey("Id");

                    b.ToTable("JobDescriptions");
                });

            modelBuilder.Entity("JobOrganization", b =>
                {
                    b.Property<Guid>("JobsId")
                        .HasColumnType("uuid");

                    b.Property<string>("OrganizationsId")
                        .HasColumnType("text");

                    b.HasKey("JobsId", "OrganizationsId");

                    b.HasIndex("OrganizationsId");

                    b.ToTable("JobOrganization");
                });

            modelBuilder.Entity("JobSkill", b =>
                {
                    b.Property<Guid>("JobsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SkillsId")
                        .HasColumnType("uuid");

                    b.HasKey("JobsId", "SkillsId");

                    b.HasIndex("SkillsId");

                    b.ToTable("JobSkill");
                });

            modelBuilder.Entity("JobUser", b =>
                {
                    b.Property<Guid>("JobsId")
                        .HasColumnType("uuid");

                    b.Property<string>("UsersId")
                        .HasColumnType("text");

                    b.HasKey("JobsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("JobUser");
                });

            modelBuilder.Entity("Skill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<string>("Type")
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("talenthubBE.Models.Comments.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("CommentText")
                        .HasColumnType("text")
                        .HasColumnName("comment_text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("DeveloperId")
                        .HasColumnType("uuid")
                        .HasColumnName("developer_id");

                    b.Property<string>("UserEmail")
                        .HasColumnType("text")
                        .HasColumnName("user_email");

                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("DeveloperId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("talenthubBE.Models.Organization", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.HasKey("Id");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("talenthubBE.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<string>("OrganizationId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Developer", b =>
                {
                    b.HasOne("talenthubBE.Models.Organization", "Organization")
                        .WithMany("Developers")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("DeveloperSkill", b =>
                {
                    b.HasOne("Developer", null)
                        .WithMany()
                        .HasForeignKey("DevelopersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Skill", null)
                        .WithMany()
                        .HasForeignKey("SkillsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DeveloperUser", b =>
                {
                    b.HasOne("Developer", null)
                        .WithMany()
                        .HasForeignKey("DevelopersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("talenthubBE.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JobOrganization", b =>
                {
                    b.HasOne("Job", null)
                        .WithMany()
                        .HasForeignKey("JobsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("talenthubBE.Models.Organization", null)
                        .WithMany()
                        .HasForeignKey("OrganizationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JobSkill", b =>
                {
                    b.HasOne("Job", null)
                        .WithMany()
                        .HasForeignKey("JobsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Skill", null)
                        .WithMany()
                        .HasForeignKey("SkillsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JobUser", b =>
                {
                    b.HasOne("Job", null)
                        .WithMany()
                        .HasForeignKey("JobsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("talenthubBE.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("talenthubBE.Models.Comments.Comment", b =>
                {
                    b.HasOne("Developer", "Developer")
                        .WithMany("Comments")
                        .HasForeignKey("DeveloperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("talenthubBE.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId");

                    b.Navigation("Developer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("talenthubBE.Models.User", b =>
                {
                    b.HasOne("talenthubBE.Models.Organization", "Organization")
                        .WithMany("Users")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("Developer", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("talenthubBE.Models.Organization", b =>
                {
                    b.Navigation("Developers");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("talenthubBE.Models.User", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
