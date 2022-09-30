﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StreamService.Infrastructure;

#nullable disable

namespace StreamService.Infrastructure.Migrations
{
    [DbContext(typeof(UploadDbContext))]
    [Migration("20220825063347_AddPropertie Key")]
    partial class AddPropertieKey
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("StreamService.Domain.Entities.UploadItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileSHA256Hash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("FileSizeInBytes")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"), false);

                    b.ToTable("T_UploadItem", (string)null);
                });

            modelBuilder.Entity("StreamService.Domain.Entities.UploadUri", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UploadItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Uri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UrlType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"), false);

                    b.HasIndex("UploadItemId");

                    b.ToTable("T_UploadUrl", (string)null);
                });

            modelBuilder.Entity("StreamService.Domain.Entities.UploadUri", b =>
                {
                    b.HasOne("StreamService.Domain.Entities.UploadItem", "UploadItem")
                        .WithMany("Uris")
                        .HasForeignKey("UploadItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UploadItem");
                });

            modelBuilder.Entity("StreamService.Domain.Entities.UploadItem", b =>
                {
                    b.Navigation("Uris");
                });
#pragma warning restore 612, 618
        }
    }
}
