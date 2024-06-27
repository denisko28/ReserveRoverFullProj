﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;
using ReserveRoverDAL;

#nullable disable

namespace ReserveRoverDAL.Migrations
{
    [DbContext(typeof(ReserveRoverDbContext))]
    [Migration("20240623161101_RemovedUserIdFixedLengthConstraint")]
    partial class RemovedUserIdFixedLengthConstraint
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ReserveRoverDAL.Entities.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("User1Id")
                        .IsRequired()
                        .HasMaxLength(28)
                        .HasColumnType("character(28)")
                        .HasColumnName("user1_id");

                    b.Property<string>("User2Id")
                        .IsRequired()
                        .HasMaxLength(28)
                        .HasColumnType("character(28)")
                        .HasColumnName("user2_id");

                    b.HasKey("Id")
                        .HasName("chats_pkey");

                    b.HasIndex("User2Id");

                    b.HasIndex("User1Id", "User2Id")
                        .IsUnique();

                    b.ToTable("chats", (string)null);
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.ChatMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChatId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateTime")
                        .HasMaxLength(120)
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_time");

                    b.Property<string>("FromUserId")
                        .IsRequired()
                        .HasMaxLength(28)
                        .HasColumnType("character varying(28)")
                        .HasColumnName("from_user_id");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)")
                        .HasColumnName("message");

                    b.Property<bool>("Viewed")
                        .HasColumnType("boolean")
                        .HasColumnName("viewed");

                    b.HasKey("Id")
                        .HasName("chats_messages_pkey");

                    b.HasIndex("ChatId");

                    b.ToTable("chats_messages", (string)null);
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("cities_pkey");

                    b.ToTable("cities", (string)null);
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.Friendship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Accepted")
                        .HasColumnType("boolean")
                        .HasColumnName("accepted");

                    b.Property<DateTime>("RequestedDateTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("requested_date_time");

                    b.Property<string>("User1Id")
                        .IsRequired()
                        .HasMaxLength(28)
                        .HasColumnType("character(28)")
                        .HasColumnName("user1_id");

                    b.Property<string>("User2Id")
                        .IsRequired()
                        .HasMaxLength(28)
                        .HasColumnType("character(28)")
                        .HasColumnName("user2_id");

                    b.HasKey("Id")
                        .HasName("friendships_pkey");

                    b.HasIndex("User2Id");

                    b.HasIndex("User1Id", "User2Id")
                        .IsUnique();

                    b.ToTable("friendships", (string)null);
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.Location", b =>
                {
                    b.Property<int>("PlaceId")
                        .HasColumnType("integer")
                        .HasColumnName("place_id");

                    b.Property<decimal>("Latitude")
                        .HasPrecision(8, 6)
                        .HasColumnType("numeric(8,6)")
                        .HasColumnName("latitude");

                    b.Property<decimal>("Longitude")
                        .HasPrecision(8, 6)
                        .HasColumnType("numeric(8,6)")
                        .HasColumnName("longitude");

                    b.HasKey("PlaceId")
                        .HasName("locations_pkey");

                    b.ToTable("locations", (string)null);
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.Moderation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date");

                    b.Property<string>("ModeratorId")
                        .IsRequired()
                        .HasMaxLength(28)
                        .HasColumnType("character varying(28)")
                        .HasColumnName("moderator_id");

                    b.Property<int>("PlaceId")
                        .HasColumnType("integer")
                        .HasColumnName("place_id");

                    b.Property<short>("Status")
                        .HasColumnType("smallint")
                        .HasColumnName("status");

                    b.HasKey("Id")
                        .HasName("moderation_pkey");

                    b.HasIndex("PlaceId");

                    b.ToTable("moderation", (string)null);
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.Place", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)")
                        .HasColumnName("address");

                    b.Property<decimal>("AvgBill")
                        .HasPrecision(7, 2)
                        .HasColumnType("numeric(7,2)")
                        .HasColumnName("avg_bill");

                    b.Property<decimal?>("AvgMark")
                        .HasPrecision(2, 1)
                        .HasColumnType("numeric(2,1)")
                        .HasColumnName("avg_mark");

                    b.Property<int>("CityId")
                        .HasColumnType("integer")
                        .HasColumnName("city_id");

                    b.Property<TimeOnly>("ClosesAt")
                        .HasColumnType("time without time zone")
                        .HasColumnName("closes_at");

                    b.Property<short>("ImagesCount")
                        .HasColumnType("smallint");

                    b.Property<string>("MainImageUrl")
                        .IsRequired()
                        .HasMaxLength(105)
                        .HasColumnType("character varying(105)")
                        .HasColumnName("main_image_url");

                    b.Property<string>("ManagerId")
                        .IsRequired()
                        .HasMaxLength(28)
                        .HasColumnType("character varying(28)")
                        .HasColumnName("manager_id");

                    b.Property<short>("ModerationStatus")
                        .HasColumnType("smallint")
                        .HasColumnName("moderation_status");

                    b.Property<TimeOnly>("OpensAt")
                        .HasColumnType("time without time zone")
                        .HasColumnName("opens_at");

                    b.Property<int>("Popularity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<DateOnly?>("PublicDate")
                        .HasColumnType("date")
                        .HasColumnName("public_date");

                    b.Property<NpgsqlTsVector>("SearchVector")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("tsvector")
                        .HasAnnotation("Npgsql:TsVectorConfig", "english")
                        .HasAnnotation("Npgsql:TsVectorProperties", new[] { "Title" });

                    b.Property<DateTime>("SubmissionDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("places_pkey");

                    b.HasIndex("CityId");

                    b.HasIndex("SearchVector");

                    NpgsqlIndexBuilderExtensions.HasMethod(b.HasIndex("SearchVector"), "GIN");

                    b.ToTable("places", (string)null);
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.PlaceDescription", b =>
                {
                    b.Property<int>("PlaceId")
                        .HasColumnType("integer")
                        .HasColumnName("place_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1500)
                        .HasColumnType("character varying(1500)")
                        .HasColumnName("description");

                    b.HasKey("PlaceId")
                        .HasName("places_descriptions_pkey");

                    b.ToTable("places_descriptions", (string)null);
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.PlaceImage", b =>
                {
                    b.Property<int>("PlaceId")
                        .HasColumnType("integer")
                        .HasColumnName("place_id");

                    b.Property<short>("SequenceIndex")
                        .HasColumnType("smallint")
                        .HasColumnName("sequence_index");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(105)
                        .HasColumnType("character varying(105)")
                        .HasColumnName("image_url");

                    b.HasKey("PlaceId", "SequenceIndex")
                        .HasName("place_images_pkey");

                    b.ToTable("place_images", (string)null);
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.PlacePaymentMethod", b =>
                {
                    b.Property<int>("PlaceId")
                        .HasColumnType("integer")
                        .HasColumnName("place_id");

                    b.Property<short>("Method")
                        .HasColumnType("smallint")
                        .HasColumnName("method");

                    b.HasKey("PlaceId", "Method")
                        .HasName("place_payment_methods_pkey");

                    b.ToTable("place_payment_methods", (string)null);
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.PublicUser", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(28)
                        .HasColumnType("character(28)")
                        .HasColumnName("id")
                        .IsFixedLength();

                    b.Property<string>("Avatar")
                        .HasMaxLength(170)
                        .HasColumnType("character varying(170)")
                        .HasColumnName("avatar");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("last_name");

                    b.HasKey("Id")
                        .HasName("public_users_pkey");

                    b.ToTable("public_users", (string)null);
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.Reservation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<TimeOnly>("BeginTime")
                        .HasColumnType("time without time zone")
                        .HasColumnName("begin_time");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("creation_date_time");

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("time without time zone")
                        .HasColumnName("end_time");

                    b.Property<short>("PeopleNum")
                        .HasColumnType("smallint")
                        .HasColumnName("people_num");

                    b.Property<int>("PlaceId")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("ReservDate")
                        .HasColumnType("date")
                        .HasColumnName("reserv_date");

                    b.Property<short>("Status")
                        .HasColumnType("smallint")
                        .HasColumnName("status");

                    b.Property<int>("TableSetId")
                        .HasColumnType("integer")
                        .HasColumnName("table_id");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(28)
                        .HasColumnType("character(28)")
                        .HasColumnName("user_id")
                        .IsFixedLength();

                    b.HasKey("Id")
                        .HasName("reservations_pkey");

                    b.HasIndex("TableSetId");

                    b.ToTable("reservations", (string)null);
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("AuthorId")
                        .IsRequired()
                        .HasMaxLength(28)
                        .HasColumnType("character varying(28)")
                        .HasColumnName("author_id");

                    b.Property<string>("Comment")
                        .HasMaxLength(5000)
                        .HasColumnType("character varying(5000)")
                        .HasColumnName("comment");

                    b.Property<DateOnly>("CreationDate")
                        .HasColumnType("date")
                        .HasColumnName("creation_date");

                    b.Property<decimal>("Mark")
                        .HasPrecision(1)
                        .HasColumnType("numeric(1)")
                        .HasColumnName("mark");

                    b.Property<int>("PlaceId")
                        .HasColumnType("integer")
                        .HasColumnName("place_id");

                    b.HasKey("Id")
                        .HasName("reviews_pkey");

                    b.HasIndex("PlaceId");

                    b.ToTable("reviews", (string)null);
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.TableSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("PlaceId")
                        .HasColumnType("integer")
                        .HasColumnName("place_id");

                    b.Property<short>("TableCapacity")
                        .HasColumnType("smallint")
                        .HasColumnName("table_type");

                    b.Property<short>("TablesNum")
                        .HasColumnType("smallint")
                        .HasColumnName("tables_num");

                    b.HasKey("Id")
                        .HasName("tables_pkey");

                    b.HasIndex("PlaceId");

                    b.ToTable("tables", (string)null);
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.Chat", b =>
                {
                    b.HasOne("ReserveRoverDAL.Entities.PublicUser", "User1")
                        .WithMany("ChatsUser1")
                        .HasForeignKey("User1Id")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired()
                        .HasConstraintName("chat_public_user1_fkey");

                    b.HasOne("ReserveRoverDAL.Entities.PublicUser", "User2")
                        .WithMany("ChatsUser2")
                        .HasForeignKey("User2Id")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired()
                        .HasConstraintName("chat_public_user2_fkey");

                    b.Navigation("User1");

                    b.Navigation("User2");
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.ChatMessage", b =>
                {
                    b.HasOne("ReserveRoverDAL.Entities.Chat", "Chat")
                        .WithMany("ChatMessages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("chat_chat_messages_id_fkey");

                    b.Navigation("Chat");
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.Friendship", b =>
                {
                    b.HasOne("ReserveRoverDAL.Entities.PublicUser", "User1")
                        .WithMany("FriendshipsUser1")
                        .HasForeignKey("User1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("friendship_public_user1_fkey");

                    b.HasOne("ReserveRoverDAL.Entities.PublicUser", "User2")
                        .WithMany("FriendshipsUser2")
                        .HasForeignKey("User2Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("friendship_public_user2_fkey");

                    b.Navigation("User1");

                    b.Navigation("User2");
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.Location", b =>
                {
                    b.HasOne("ReserveRoverDAL.Entities.Place", "Place")
                        .WithOne("Location")
                        .HasForeignKey("ReserveRoverDAL.Entities.Location", "PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("locations_place_id_fkey");

                    b.Navigation("Place");
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.Moderation", b =>
                {
                    b.HasOne("ReserveRoverDAL.Entities.Place", "Place")
                        .WithMany("Moderations")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("moderation_place_id_fkey");

                    b.Navigation("Place");
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.Place", b =>
                {
                    b.HasOne("ReserveRoverDAL.Entities.City", "City")
                        .WithMany("Places")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired()
                        .HasConstraintName("places_city_id_fkey");

                    b.Navigation("City");
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.PlaceDescription", b =>
                {
                    b.HasOne("ReserveRoverDAL.Entities.Place", "Place")
                        .WithOne("PlaceDescription")
                        .HasForeignKey("ReserveRoverDAL.Entities.PlaceDescription", "PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("places_descriptions_place_id_fkey");

                    b.Navigation("Place");
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.PlaceImage", b =>
                {
                    b.HasOne("ReserveRoverDAL.Entities.Place", "Place")
                        .WithMany("PlaceImages")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("place_images_place_id_fkey");

                    b.Navigation("Place");
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.PlacePaymentMethod", b =>
                {
                    b.HasOne("ReserveRoverDAL.Entities.Place", "Place")
                        .WithMany("PlacePaymentMethods")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("place_payment_methods_place_id_fkey");

                    b.Navigation("Place");
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.Reservation", b =>
                {
                    b.HasOne("ReserveRoverDAL.Entities.TableSet", "TableSet")
                        .WithMany("Reservations")
                        .HasForeignKey("TableSetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("reservations_table_id_fkey");

                    b.Navigation("TableSet");
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.Review", b =>
                {
                    b.HasOne("ReserveRoverDAL.Entities.Place", "Place")
                        .WithMany("Reviews")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("reviews_place_id_fkey");

                    b.Navigation("Place");
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.TableSet", b =>
                {
                    b.HasOne("ReserveRoverDAL.Entities.Place", "Place")
                        .WithMany("TableSets")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("tables_place_id_fkey");

                    b.Navigation("Place");
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.Chat", b =>
                {
                    b.Navigation("ChatMessages");
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.City", b =>
                {
                    b.Navigation("Places");
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.Place", b =>
                {
                    b.Navigation("Location");

                    b.Navigation("Moderations");

                    b.Navigation("PlaceDescription")
                        .IsRequired();

                    b.Navigation("PlaceImages");

                    b.Navigation("PlacePaymentMethods");

                    b.Navigation("Reviews");

                    b.Navigation("TableSets");
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.PublicUser", b =>
                {
                    b.Navigation("ChatsUser1");

                    b.Navigation("ChatsUser2");

                    b.Navigation("FriendshipsUser1");

                    b.Navigation("FriendshipsUser2");
                });

            modelBuilder.Entity("ReserveRoverDAL.Entities.TableSet", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
