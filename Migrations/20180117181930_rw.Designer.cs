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
    [Migration("20180117181930_rw")]
    partial class rw
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("rgz.Models.Good", b =>
                {
                    b.Property<int>("GoodId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImgPath");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.HasKey("GoodId");

                    b.ToTable("Goods");
                });
#pragma warning restore 612, 618
        }
    }
}
