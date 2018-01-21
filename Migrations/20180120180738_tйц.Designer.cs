﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using rgz.Models;
using System;

namespace rgz.Migrations
{
    [DbContext(typeof(ShopDB))]
    [Migration("20180120180738_tйц")]
    partial class tйц
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("rgz.Models.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Adress")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ClientId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("rgz.Models.ClientGood", b =>
                {
                    b.Property<int>("ClientId");

                    b.Property<int>("GoodId");

                    b.Property<int>("Quantity");

                    b.HasKey("ClientId", "GoodId");

                    b.HasIndex("GoodId");

                    b.ToTable("ClientGood");
                });

            modelBuilder.Entity("rgz.Models.Good", b =>
                {
                    b.Property<int>("GoodId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("ImgPath");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.HasKey("GoodId");

                    b.ToTable("Goods");
                });

            modelBuilder.Entity("rgz.Models.ClientGood", b =>
                {
                    b.HasOne("rgz.Models.Client", "Client")
                        .WithMany("ClientGood")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("rgz.Models.Good", "Good")
                        .WithMany("ClientGood")
                        .HasForeignKey("GoodId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
