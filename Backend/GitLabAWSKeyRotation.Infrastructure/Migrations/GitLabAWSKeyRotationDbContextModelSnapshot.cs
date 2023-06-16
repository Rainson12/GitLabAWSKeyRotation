﻿// <auto-generated />
using System;
using GitLabAWSKeyRotation.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GitLabAWSKeyRotation.Infrastructure.Migrations
{
    [DbContext(typeof(GitLabAWSKeyRotationDbContext))]
    partial class GitLabAWSKeyRotationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GitLabAWSKeyRotation.Domain.AWS.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("AccountId");

                    b.Property<string>("AwsAccountId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("AWSAccounts", (string)null);
                });

            modelBuilder.Entity("GitLabAWSKeyRotation.Domain.AWS.Entities.IAM", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("IamId");

                    b.Property<string>("AccessKeyId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("AccessSecret")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Arn")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<float>("KeyRotationInDays")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("AWSIAMs", (string)null);
                });

            modelBuilder.Entity("GitLabAWSKeyRotation.Domain.Application.Application", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("AccessTokenId");

                    b.Property<string>("AuthKey")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("GitLabAWSKeyRotation.Domain.GitLab.AccessToken", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("AccessTokenId");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TokenName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("GitlabAccessTokens");
                });

            modelBuilder.Entity("GitLabAWSKeyRotation.Domain.GitLab.CodeRepository", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("CodeRepositoryId");

                    b.Property<Guid>("AccessTokenId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AccessTokenId");

                    b.ToTable("CodeRepository");
                });

            modelBuilder.Entity("GitLabAWSKeyRotation.Domain.GitLab.Rotation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)")
                        .HasColumnName("RotationDependentRepositoryVariablesId");

                    b.Property<string>("AccessKeyIdVariableName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("AccessSecretVariableName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("CodeRepositoryId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Environment")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("IAMId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("CodeRepositoryId");

                    b.HasIndex("IAMId");

                    b.ToTable("RotationDependentRepositoryVariables", (string)null);
                });

            modelBuilder.Entity("GitLabAWSKeyRotation.Domain.AWS.Entities.IAM", b =>
                {
                    b.HasOne("GitLabAWSKeyRotation.Domain.AWS.Account", null)
                        .WithMany("IamIdentities")
                        .HasForeignKey("AccountId");
                });

            modelBuilder.Entity("GitLabAWSKeyRotation.Domain.GitLab.CodeRepository", b =>
                {
                    b.HasOne("GitLabAWSKeyRotation.Domain.GitLab.AccessToken", null)
                        .WithMany("CodeRepositories")
                        .HasForeignKey("AccessTokenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GitLabAWSKeyRotation.Domain.GitLab.Rotation", b =>
                {
                    b.HasOne("GitLabAWSKeyRotation.Domain.GitLab.CodeRepository", null)
                        .WithMany()
                        .HasForeignKey("CodeRepositoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GitLabAWSKeyRotation.Domain.AWS.Entities.IAM", null)
                        .WithMany("Rotations")
                        .HasForeignKey("IAMId");
                });

            modelBuilder.Entity("GitLabAWSKeyRotation.Domain.AWS.Account", b =>
                {
                    b.Navigation("IamIdentities");
                });

            modelBuilder.Entity("GitLabAWSKeyRotation.Domain.AWS.Entities.IAM", b =>
                {
                    b.Navigation("Rotations");
                });

            modelBuilder.Entity("GitLabAWSKeyRotation.Domain.GitLab.AccessToken", b =>
                {
                    b.Navigation("CodeRepositories");
                });
#pragma warning restore 612, 618
        }
    }
}
