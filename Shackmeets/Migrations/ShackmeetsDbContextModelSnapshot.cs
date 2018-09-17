﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shackmeets.Models;

namespace Shackmeets.Migrations
{
    [DbContext(typeof(ShackmeetsDbContext))]
    partial class ShackmeetsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Shackmeets.Models.Meet", b =>
                {
                    b.Property<int>("MeetId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<DateTime>("EventDate");

                    b.Property<bool>("IsCancelled");

                    b.Property<DateTime?>("LastAnnouncementPostDate");

                    b.Property<string>("LocationAddress");

                    b.Property<string>("LocationCountry");

                    b.Property<decimal>("LocationLatitude");

                    b.Property<decimal>("LocationLongitude");

                    b.Property<string>("LocationName");

                    b.Property<string>("LocationState");

                    b.Property<string>("Name");

                    b.Property<string>("OrganizerUsername");

                    b.Property<DateTime?>("TimestampChange");

                    b.Property<DateTime>("TimestampCreate");

                    b.Property<bool>("WillPostAnnouncement");

                    b.HasKey("MeetId");

                    b.HasIndex("OrganizerUsername");

                    b.ToTable("Meets");
                });

            modelBuilder.Entity("Shackmeets.Models.Notification", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsSent");

                    b.Property<int?>("MeetId");

                    b.Property<string>("MessageBody");

                    b.Property<string>("MessageSubject");

                    b.Property<int>("NotificationReason");

                    b.Property<int>("NotificationType");

                    b.Property<string>("TargetUserUsername");

                    b.Property<string>("TargetUsername");

                    b.Property<DateTime>("TimestampCreate");

                    b.HasKey("NotificationId");

                    b.HasIndex("MeetId");

                    b.HasIndex("TargetUserUsername");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("Shackmeets.Models.Rsvp", b =>
                {
                    b.Property<int>("RsvpId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MeetId");

                    b.Property<int>("NumAttendees");

                    b.Property<int>("RsvpTypeId");

                    b.Property<string>("Username");

                    b.HasKey("RsvpId");

                    b.HasIndex("MeetId");

                    b.HasIndex("Username");

                    b.ToTable("Rsvps");
                });

            modelBuilder.Entity("Shackmeets.Models.User", b =>
                {
                    b.Property<string>("Username")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsAdmin");

                    b.Property<bool>("IsBanned");

                    b.Property<decimal>("LocationLatitude");

                    b.Property<decimal>("LocationLongitude");

                    b.Property<int>("MaxNotificationDistance");

                    b.Property<string>("NotificationEmail");

                    b.Property<int>("NotificationOption");

                    b.Property<bool>("NotifyByEmail");

                    b.Property<bool>("NotifyByShackmessage");

                    b.HasKey("Username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Shackmeets.Models.Meet", b =>
                {
                    b.HasOne("Shackmeets.Models.User", "Organizer")
                        .WithMany("Meets")
                        .HasForeignKey("OrganizerUsername");
                });

            modelBuilder.Entity("Shackmeets.Models.Notification", b =>
                {
                    b.HasOne("Shackmeets.Models.Meet", "Meet")
                        .WithMany()
                        .HasForeignKey("MeetId");

                    b.HasOne("Shackmeets.Models.User", "TargetUser")
                        .WithMany()
                        .HasForeignKey("TargetUserUsername");
                });

            modelBuilder.Entity("Shackmeets.Models.Rsvp", b =>
                {
                    b.HasOne("Shackmeets.Models.Meet", "Meet")
                        .WithMany("Rsvps")
                        .HasForeignKey("MeetId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Shackmeets.Models.User", "User")
                        .WithMany("Rsvps")
                        .HasForeignKey("Username");
                });
#pragma warning restore 612, 618
        }
    }
}
