﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReactBlog.Spa.Data;

namespace ReactBlog.Spa.Migrations
{
    [DbContext(typeof(BlogContext))]
    [Migration("20210223182851_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReactBlog.Spa.Models.BlogPost", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("AuthorEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ingress")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTimeOffset>("Published")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("BlogPost");

                    b.HasData(
                        new
                        {
                            Id = new Guid("499b3085-f1e3-4bc2-be85-ab97f44005c9"),
                            Author = "Professor X",
                            AuthorEmail = "charles.xavier@x-men.org",
                            Ingress = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                            Published = new DateTimeOffset(new DateTime(2021, 1, 23, 18, 28, 50, 744, DateTimeKind.Unspecified).AddTicks(1570), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Nullam egestas nisi turpis, ut interdum ante lobortis at. Praesent euismod consequat quam quis vulputate. Aenean convallis et arcu vel lobortis. Cras eget augue non ipsum efficitur viverra ac in velit. Quisque aliquet porttitor enim, eu consectetur sem vehicula et. In facilisis dolor odio, eu ornare massa placerat quis. Proin quis tincidunt tellus. Nulla mattis congue arcu id imperdiet. Sed rhoncus venenatis egestas. Etiam ultrices leo tempus justo fringilla feugiat. Sed non quam malesuada, congue velit quis, maximus sem. Cras laoreet eleifend est, eget lobortis erat sollicitudin id. Sed ac mi enim. Mauris pulvinar turpis non vulputate mollis.",
                            Title = "Donec eu eros massa"
                        },
                        new
                        {
                            Id = new Guid("9cbc9601-abac-49ec-8a94-154a899563fd"),
                            Author = "Cyclops",
                            AuthorEmail = "scott.summers@x-men.org",
                            Ingress = "Proin id lectus dui. Praesent ac metus sit amet risus varius egestas porttitor eu metus.",
                            Published = new DateTimeOffset(new DateTime(2020, 12, 23, 18, 28, 50, 745, DateTimeKind.Unspecified).AddTicks(173), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Aenean varius commodo magna sit amet interdum. Praesent pharetra eu justo laoreet placerat. Proin eget pellentesque turpis. Aliquam finibus felis eget hendrerit malesuada. Donec porttitor volutpat ex a varius. Nam porttitor nisl nec sem semper tristique. Nulla et nisl sapien. Praesent at iaculis lacus, eget hendrerit purus. Quisque tristique vehicula ante sit amet convallis. Donec rhoncus eget odio vel efficitur. Cras dui elit, fringilla ac lorem at, semper interdum turpis. Integer pellentesque diam aliquam purus tristique, non ultricies ipsum venenatis. Proin vitae ipsum nec nibh venenatis porttitor. Cras vel luctus lorem, vel placerat massa. Quisque sit amet molestie diam, et fermentum est.",
                            Title = "Cras sed ornare lectus"
                        },
                        new
                        {
                            Id = new Guid("e06db771-ad09-4796-9eff-f491f5a3f12b"),
                            Author = "Storm",
                            AuthorEmail = "ororo.munroe@x-men.org",
                            Ingress = "Interdum et malesuada fames ac ante ipsum primis in faucibus.",
                            Published = new DateTimeOffset(new DateTime(2020, 11, 23, 18, 28, 50, 745, DateTimeKind.Unspecified).AddTicks(279), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Nam tristique vel diam et mattis. Curabitur rutrum vestibulum mattis. Etiam egestas porta sem, ut consectetur magna commodo ac. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Mauris convallis efficitur tincidunt. In vitae luctus ligula, sit amet consequat orci. Proin eleifend, mi pretium dignissim porta, sapien ligula malesuada risus, eget consectetur tellus orci a lacus. Praesent imperdiet elit id mauris tempor, nec venenatis leo facilisis. Ut malesuada semper enim, quis facilisis quam convallis a. Suspendisse efficitur eleifend vestibulum. Quisque sed varius metus, dictum faucibus mi. Nam ac sem ante.",
                            Title = "Proin id lectus dui"
                        },
                        new
                        {
                            Id = new Guid("52c7f2b5-08b1-4023-97aa-9348680e7ab8"),
                            Author = "Colossus",
                            AuthorEmail = "piotr.rasputin@x-men.org",
                            Ingress = "Integer feugiat sem vel massa faucibus vehicula.",
                            Published = new DateTimeOffset(new DateTime(2020, 10, 23, 18, 28, 50, 745, DateTimeKind.Unspecified).AddTicks(294), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Suspendisse vitae venenatis ante, eu dictum sem. Nam pharetra diam urna, quis rhoncus justo porta in. Aliquam tristique lacinia ligula. Vivamus ultricies sapien pellentesque ligula aliquam cursus. Integer porta lobortis nunc id vestibulum. Nulla gravida, mauris ac faucibus gravida, magna sapien tempor ante, vel condimentum purus dolor eu lectus. Interdum et malesuada fames ac ante ipsum primis in faucibus. Cras id risus a ante varius aliquet. Pellentesque vitae enim elementum, malesuada sem ut, congue mi. Nunc in lobortis dui, a sagittis orci.",
                            Title = "Fusce sed nunc mauris"
                        },
                        new
                        {
                            Id = new Guid("b16d85c5-f4ec-41d1-80da-069cb12b79fd"),
                            Author = "Beast",
                            AuthorEmail = "henry.mccoy@x-men.org",
                            Ingress = "Duis rutrum placerat orci vel venenatis.",
                            Published = new DateTimeOffset(new DateTime(2020, 9, 23, 18, 28, 50, 745, DateTimeKind.Unspecified).AddTicks(306), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Nulla fermentum varius tortor eu sagittis. Nullam sed urna eu magna egestas elementum et sed velit. Vivamus tincidunt facilisis ante vel feugiat. Suspendisse porttitor sapien in felis viverra, vitae tincidunt nibh dapibus. Suspendisse potenti. Maecenas iaculis ante eget erat accumsan, eget hendrerit sapien vestibulum. Nulla pretium, massa a cursus interdum, nibh risus faucibus urna, et elementum neque diam ac augue. Nulla lacus neque, interdum et urna eget, bibendum dignissim nulla. Vestibulum ac laoreet dui, a placerat ipsum. Ut iaculis consequat condimentum.",
                            Title = "Curabitur id erat erat"
                        },
                        new
                        {
                            Id = new Guid("704f7b2e-2a3d-472c-a5a7-2c0076840664"),
                            Author = "Phoenix",
                            AuthorEmail = "jean.grey@x-men.org",
                            Ingress = "Pellentesque est nisi, fringilla non ante id, tempus sollicitudin ipsum.",
                            Published = new DateTimeOffset(new DateTime(2020, 8, 23, 18, 28, 50, 745, DateTimeKind.Unspecified).AddTicks(322), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Mauris feugiat fringilla neque, eget volutpat urna convallis porta. Integer accumsan orci vel congue pulvinar. Quisque gravida nibh ut pulvinar consequat. Nam non lacus id lacus vehicula ornare sed in risus. Praesent ut eleifend est. Integer ac feugiat dolor, bibendum dapibus risus. Etiam eget ligula consectetur, sagittis quam a, accumsan erat. Praesent velit mauris, condimentum nec porttitor eu, tempor quis libero. Aenean pretium mauris quis nunc laoreet, sit amet suscipit odio volutpat. Ut vehicula dignissim velit, in mattis mi fermentum et. Ut luctus magna vitae suscipit tincidunt. Etiam at tortor rhoncus, dignissim magna et, volutpat ipsum. Fusce a leo sapien. Nulla a rhoncus nisl, vitae elementum risus.",
                            Title = "Nulla facilisi"
                        },
                        new
                        {
                            Id = new Guid("af44d552-7930-4679-8057-9a9de83ccf50"),
                            Author = "Polaris",
                            AuthorEmail = "lorna.dane@x-men.org",
                            Ingress = "Nulla varius odio sit amet sagittis tincidunt.",
                            Published = new DateTimeOffset(new DateTime(2020, 7, 23, 18, 28, 50, 745, DateTimeKind.Unspecified).AddTicks(333), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Cras eu dapibus erat, vitae faucibus lacus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Mauris venenatis, nulla id posuere posuere, lectus leo interdum est, id vulputate dui eros in odio. Nunc auctor, ligula porta consequat auctor, elit purus convallis libero, et hendrerit nibh nisl eu risus. Proin ut consequat nisi. Mauris vitae ullamcorper metus. Proin dapibus ullamcorper nisi ut consequat. In non mauris dui. Ut vitae arcu vitae sapien consequat placerat et nec nulla. Maecenas varius tortor sed condimentum condimentum. Nulla eu massa eget purus scelerisque iaculis vestibulum vel nunc. Morbi venenatis magna odio, eget dapibus ex vulputate non.",
                            Title = "Proin at aliquam urna"
                        },
                        new
                        {
                            Id = new Guid("d4ea0842-ddf8-4240-afc7-b458d7348eb7"),
                            Author = "Nightcrawler",
                            AuthorEmail = "kurt.wagner@x-men.org",
                            Ingress = "Interdum et malesuada fames ac ante ipsum primis in faucibus.",
                            Published = new DateTimeOffset(new DateTime(2020, 6, 23, 18, 28, 50, 745, DateTimeKind.Unspecified).AddTicks(344), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Suspendisse nulla libero, dapibus a ipsum nec, ullamcorper pharetra nibh. Phasellus nec libero turpis. Donec vestibulum sit amet massa at imperdiet. Curabitur est ligula, fermentum non bibendum luctus, imperdiet placerat leo. Proin nec pellentesque nulla. Vestibulum lobortis, tellus vitae viverra molestie, dolor augue rutrum sapien, eget rhoncus quam risus eget risus. Curabitur id hendrerit quam, et aliquet dolor. Nam vehicula venenatis augue a volutpat. Nulla scelerisque ante at porta pulvinar. Duis malesuada congue pulvinar. Suspendisse a nisl et velit vestibulum rhoncus. Suspendisse purus arcu, accumsan vitae lacinia quis, maximus sed leo. Sed egestas libero in accumsan ullamcorper. Donec non neque in sapien viverra accumsan ac vel est.",
                            Title = "Proin quis tincidunt tellus"
                        },
                        new
                        {
                            Id = new Guid("30d2c9e8-3b9d-4412-9c71-8be17eb47e2f"),
                            Author = "Shadowcat",
                            AuthorEmail = "kitty.pryde@x-men.org",
                            Ingress = "Etiam gravida ex id felis imperdiet laoreet.",
                            Published = new DateTimeOffset(new DateTime(2020, 5, 23, 18, 28, 50, 745, DateTimeKind.Unspecified).AddTicks(359), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Integer pulvinar maximus pellentesque. Praesent est nibh, lobortis id nunc a, porta accumsan orci. Maecenas tempor pharetra tellus, ac sollicitudin purus laoreet vitae. Phasellus tristique purus ut leo rhoncus commodo. Phasellus efficitur nibh leo, id varius mi ornare et. Aenean suscipit, mauris a consectetur convallis, dolor nunc fringilla lorem, non dapibus erat nulla in lorem. Vivamus consequat et velit non vestibulum. Aenean eu nulla vitae neque vulputate elementum. Mauris laoreet urna et erat semper, non tempor quam volutpat. Vivamus consequat, massa eget mollis consectetur, tellus tortor consequat enim, vulputate vulputate mauris orci et velit. Aliquam in lacinia ante. Sed id aliquet odio.",
                            Title = "Praesent eu semper magna"
                        },
                        new
                        {
                            Id = new Guid("861a5ef3-1f1a-42cb-94fc-812d39f311d6"),
                            Author = "Rogue",
                            AuthorEmail = "anna.lebeau@x-men.org",
                            Ingress = "Vivamus aliquet tellus ligula, sed rutrum elit mollis quis.",
                            Published = new DateTimeOffset(new DateTime(2020, 4, 23, 18, 28, 50, 745, DateTimeKind.Unspecified).AddTicks(372), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Etiam vitae dapibus sem. Donec ultricies, ante vitae consectetur eleifend, turpis metus blandit mi, non fermentum turpis magna eu tellus. Sed sit amet interdum libero, sed sodales ligula. Nam mauris risus, semper at molestie ut, faucibus ut mauris. Morbi id magna quis magna mattis consectetur. Maecenas vehicula ac tellus vitae venenatis. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Sed vitae fermentum ex, vitae pellentesque nunc. Sed pretium vestibulum tortor. Etiam non mollis quam. Nunc pulvinar erat massa, eu consequat dolor interdum sit amet. Quisque quis magna interdum, rutrum est vel, ornare quam. Vestibulum in mauris a leo molestie porttitor.",
                            Title = "Nullam fermentum aliquet leo"
                        },
                        new
                        {
                            Id = new Guid("943742f6-d842-4c85-9802-1078eb929170"),
                            Author = "Iceman",
                            AuthorEmail = "robert.drake@x-men.org",
                            Ingress = "Nam venenatis tellus finibus, dignissim diam et, vehicula libero.",
                            Published = new DateTimeOffset(new DateTime(2020, 3, 23, 18, 28, 50, 745, DateTimeKind.Unspecified).AddTicks(426), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "Fusce cursus, nisi a sodales tincidunt, lacus velit convallis sem, vel tempus nulla erat non nisl. Donec eu eleifend eros. Etiam scelerisque augue eget ligula porta, in condimentum augue faucibus. Aenean imperdiet suscipit rhoncus. Vestibulum vulputate ac mi et pharetra. Suspendisse potenti. Suspendisse at tempor lectus. Nam laoreet vestibulum consectetur. Mauris sagittis nisl sit amet sem pharetra vestibulum. In sit amet porta dui. Vivamus malesuada lectus justo, ullamcorper facilisis lorem commodo ac. Pellentesque scelerisque nunc et sodales pharetra.",
                            Title = "Curabitur non convallis lorem"
                        },
                        new
                        {
                            Id = new Guid("fe190d01-9b01-41d0-b72b-02e51b964482"),
                            Author = "Wolverine",
                            AuthorEmail = "logan.howlett@x-men.org",
                            Ingress = "Suspendisse suscipit orci non velit porttitor, fermentum iaculis diam aliquet.",
                            Published = new DateTimeOffset(new DateTime(2020, 2, 23, 18, 28, 50, 745, DateTimeKind.Unspecified).AddTicks(437), new TimeSpan(0, 0, 0, 0, 0)),
                            Text = "In suscipit lacus non felis tristique, at consectetur est interdum. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Duis porttitor interdum vulputate. Donec ultrices purus ut enim rhoncus, id interdum enim consequat. Aliquam ullamcorper est sit amet ipsum dignissim sollicitudin. Aenean sollicitudin congue sollicitudin. Nulla aliquet pellentesque ante sit amet auctor. Donec tellus urna, eleifend bibendum volutpat pharetra, vehicula nec mi. Ut ut leo finibus, ultrices massa a, vehicula dolor. Duis vestibulum tortor sed lacus consectetur, id vehicula metus malesuada. Nullam tincidunt ipsum ac magna pulvinar finibus.",
                            Title = "Nam sollicitudin"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
