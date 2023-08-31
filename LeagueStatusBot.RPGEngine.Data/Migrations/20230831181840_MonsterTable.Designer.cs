﻿// <auto-generated />
using LeagueStatusBot.RPGEngine.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LeagueStatusBot.RPGEngine.Data.Migrations
{
    [DbContext(typeof(GameDbContext))]
    [Migration("20230831181840_MonsterTable")]
    partial class MonsterTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("LeagueStatusBot.RPGEngine.Data.Entities.ArmorEffectEntity", b =>
                {
                    b.Property<int>("EffectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("EffectFor")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("EffectId");

                    b.ToTable("ArmorEffects");
                });

            modelBuilder.Entity("LeagueStatusBot.RPGEngine.Data.Entities.BeingEntity", b =>
                {
                    b.Property<ulong>("DiscordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Agility")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Boots")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Charisma")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Chest")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Endurance")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Gloves")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Helm")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Intelligence")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Inventory")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Legs")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Luck")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaxHitPoints")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Strength")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Weapon")
                        .HasColumnType("INTEGER");

                    b.HasKey("DiscordId");

                    b.ToTable("Beings");
                });

            modelBuilder.Entity("LeagueStatusBot.RPGEngine.Data.Entities.CampaignEntity", b =>
                {
                    b.Property<string>("MonsterName")
                        .HasColumnType("TEXT");

                    b.Property<string>("IntroductionImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("IntroductionPost")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MidPost")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MidPostImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PreFightPost")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PreFightPostImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MonsterName");

                    b.ToTable("Campaigns");
                });

            modelBuilder.Entity("LeagueStatusBot.RPGEngine.Data.Entities.ItemEffectEntity", b =>
                {
                    b.Property<int>("EffectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("EffectClass")
                        .HasColumnType("INTEGER");

                    b.Property<string>("EffectName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("EffectType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("EffectId");

                    b.ToTable("ItemEffects");
                });

            modelBuilder.Entity("LeagueStatusBot.RPGEngine.Data.Entities.ItemEntity", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemEffect")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ItemType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Rarity")
                        .HasColumnType("INTEGER");

                    b.HasKey("ItemId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("LeagueStatusBot.RPGEngine.Data.Entities.LootEntity", b =>
                {
                    b.Property<ulong>("DiscordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("LootCount")
                        .HasColumnType("INTEGER");

                    b.HasKey("DiscordId");

                    b.ToTable("Loot");
                });

            modelBuilder.Entity("LeagueStatusBot.RPGEngine.Data.Entities.SuperMonsterEntity", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Name");

                    b.ToTable("SuperMonsters");
                });

            modelBuilder.Entity("LeagueStatusBot.RPGEngine.Data.Entities.SuperMonsterEntity", b =>
                {
                    b.OwnsOne("LeagueStatusBot.Common.Models.Super", "FirstSuper", b1 =>
                        {
                            b1.Property<string>("SuperMonsterEntityName")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Damage")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Effect")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<int>("Targets")
                                .HasColumnType("INTEGER");

                            b1.HasKey("SuperMonsterEntityName");

                            b1.ToTable("SuperMonsters");

                            b1.WithOwner()
                                .HasForeignKey("SuperMonsterEntityName");
                        });

                    b.OwnsOne("LeagueStatusBot.Common.Models.Super", "SecondSuper", b1 =>
                        {
                            b1.Property<string>("SuperMonsterEntityName")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Damage")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Effect")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<int>("Targets")
                                .HasColumnType("INTEGER");

                            b1.HasKey("SuperMonsterEntityName");

                            b1.ToTable("SuperMonsters");

                            b1.WithOwner()
                                .HasForeignKey("SuperMonsterEntityName");
                        });

                    b.Navigation("FirstSuper")
                        .IsRequired();

                    b.Navigation("SecondSuper")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
