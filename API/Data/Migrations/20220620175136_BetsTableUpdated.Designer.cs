// <auto-generated />
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220620175136_BetsTableUpdated")]
    partial class BetsTableUpdated
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.5");

            modelBuilder.Entity("API.Entities.Bet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BetAmount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BetNumber")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Payout")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RollId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Winner")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Bets");
                });

            modelBuilder.Entity("API.Entities.Roll", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("RollResult")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Rolls");
                });
#pragma warning restore 612, 618
        }
    }
}
