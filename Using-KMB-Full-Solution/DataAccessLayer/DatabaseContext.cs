using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using MuhendisAsci.Web.Models;

namespace MuhendisAsci.Web.DataAccessLayer
{
    public partial class DatabaseContext : DbContext
    {
        private bool _DoNotModifyTableBaseInfo;

        public DbSet<AboutSection> AboutSections { get; set; }
        public DbSet<ContactSection> ContactSections { get; set; }
        public DbSet<PortfolioSection> PortfolioSections { get; set; }
        public DbSet<PortfolioItem> PortfolioItems { get; set; }
        public DbSet<IntroSection> IntroSections { get; set; }
        public DbSet<SectionDefine> SectionDefines { get; set; }
        public DbSet<SuperUser> Users { get; set; }
        public DbSet<PortfolioSubItem> PortfolioSubItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<OrderMessage> OrderMessages { get; set; }

        public DatabaseContext()
        {
            Database.SetInitializer<DatabaseContext>(new MyInitializer());
        }

        public int SaveChanges(bool doNotModifyTableBaseInfo)
        {
            this._DoNotModifyTableBaseInfo = doNotModifyTableBaseInfo;
            return SaveChanges();
        }

        public override int SaveChanges()
        {
            if (this._DoNotModifyTableBaseInfo)
                return base.SaveChanges();

            List<DbEntityEntry> entities =
                ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted).ToList();

            foreach (DbEntityEntry entity in entities)
            {
                if (entity.Entity is Models.DbTableBase)
                {
                    Models.DbTableBase entityBase = entity.Entity as Models.DbTableBase;
                    DateTime now = DateTime.Now;

                    SuperUser user = null;

                    if (HttpContext.Current.Session["sys_login"] != null)
                    {
                        user = HttpContext.Current.Session["sys_login"] as SuperUser;
                    }

                    switch (entity.State)
                    {
                        case EntityState.Added:
                            entityBase.CreatedOn = now;
                            entityBase.CreatedUser = user?.Username ?? "noname";
                            break;
                        case EntityState.Deleted:
                            // INFO : Eğer hiçbir kayıt silinmesin istenirse, daha sonra MyEntityBase'e eklenecek bir property ile "IsDeleted" gibi IsDeleted = true yapılarak entity.state = EntityState.Modified olarak değiştirilmeli.
                            //entityBase.ModifiedOn = now;
                            //entityBase.ModifiedUser = App.CurrentUser?.KullaniciAdi ?? string.Empty;


                            // ***************************************************************************
                            // Silinen kayıtlar günlük olarak yyyyMMdd.xml isimli bir dosyada saklanır.
                            // Sadece MyEntityBase 'den türemiş olan nesneler için bu kayıt yapılır.
                            // ***************************************************************************
                            //string fileName = DateTime.Now.ToString("yyyyMMdd") + ".xml";
                            //string filePath = HttpContext.Current.Server.MapPath(@"~\Removed\" + fileName);

                            //XDocument xdoc = null;

                            //if (File.Exists(filePath))
                            //{
                            //    xdoc = XDocument.Load(filePath);
                            //}
                            //else
                            //{
                            //    xdoc = new XDocument(
                            //        new XDeclaration("1.0", "utf-8", "no"),
                            //        new XElement("items"));
                            //}

                            //string entityXml = EntitySerializer.Serialize(entity.Entity.GetType(), entity.Entity);
                            //XElement xe = XElement.Parse(entityXml);

                            //xdoc.Root.Add(xe);
                            //xdoc.Save(filePath);
                            // ***************************************************************************
                            // ***************************************************************************

                            break;
                        case EntityState.Detached:
                            break;
                        case EntityState.Modified:
                            entityBase.ModifiedOn = now;
                            entityBase.ModifiedUser = user?.Username ?? "noname";

                            break;
                        case EntityState.Unchanged:
                            break;
                        default:
                            break;
                    }
                }
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure Student & StudentAddress entity
            modelBuilder.Entity<SectionDefine>()
                        .HasOptional(s => s.AboutSection) // Mark AboutSection property optional in SectionDefine entity
                        .WithRequired(f => f.Section); // mark Section property as required in AboutSection entity. Cannot save AboutSection without Section

            modelBuilder.Entity<SectionDefine>()
                        .HasOptional(s => s.ContactSection)
                        .WithRequired(f => f.Section);

            modelBuilder.Entity<SectionDefine>()
                        .HasOptional(s => s.IntroSection)
                        .WithRequired(f => f.Section);

            modelBuilder.Entity<SectionDefine>()
                        .HasOptional(s => s.PortfolioSection)
                        .WithRequired(f => f.Section);
        }
    }

    public partial class MyInitializer:CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            SuperUser admin = new SuperUser();
            admin.IsActive = true;
            admin.IsDefault = true;
            admin.Username = "admin";
            admin.Password = "1qaz!QAZ";
            admin.EMail = "developer@muratbaseren.com";

            context.Users.Add(admin);
            context.SaveChanges();


            SectionDefine intro1 = new SectionDefine()
            {
                Name = "intro1",
                Title = "Intro 1",
                IsVisible = true,
                OrderNumber = 1,
                SectionType = "intro",
                Info = "Bu kısım intro1 kısmıdır."
            };

            SectionDefine portfolio1 = new SectionDefine()
            {
                Name = "portfolio1",
                Title = "Portfolio 1",
                IsVisible = true,
                OrderNumber = 2,
                SectionType = "portfolio",
                Info = "Bu kısım portfolio1 kısmıdır."
            };

            SectionDefine about1 = new SectionDefine()
            {
                Name = "about1",
                Title = "About",
                IsVisible = true,
                OrderNumber = 3,
                SectionType = "about",
                Info = "Bu kısım about1 kısmıdır."
            };

            context.SectionDefines.Add(intro1);
            context.SectionDefines.Add(portfolio1);
            context.SectionDefines.Add(about1);

            context.SaveChanges();


            IntroSection introSection = new IntroSection()
            {
                Id = intro1.Id,
                ImagePath = "/uploads/cake.png",
                AltText = "Resim hakkında bilgi metni",
                Name = intro1.Name,
                Title = intro1.Title,
                SubTitle = "Alt başlık metni",
                Section = intro1
            };

            PortfolioSection portfolioSection = new PortfolioSection()
            {
                Id = portfolio1.Id,
                Name = portfolio1.Name,
                Title = portfolio1.Title,
                Section = portfolio1
            };

            AboutSection aboutSection = new AboutSection()
            {
                Id = about1.Id,
                Name = about1.Name,
                Title = about1.Title,
                InnerHtml = "Gösterilecek metin",
                Section = about1
            };

            context.IntroSections.Add(introSection);
            context.AboutSections.Add(aboutSection);
            context.PortfolioSections.Add(portfolioSection);

            context.SaveChanges();

            if(Directory.Exists(HttpContext.Current.Server.MapPath("~/sys/bin")))
            {
                if(File.Exists(HttpContext.Current.Server.MapPath("~/sys/bin/cfg.json")))
                {
                    File.Delete(HttpContext.Current.Server.MapPath("~/sys/bin/cfg.json"));
                    System.Threading.Thread.Sleep(1000);
                }
            }

            HttpContext.Current.Cache.Remove("config");
            SiteConfig.LoadConfig();

            SiteConfig.Obj.Title = "Mühendis Aşçı";
            SiteConfig.Obj.Author = "K. Murat Başeren";
            SiteConfig.Obj.AuthorUrl = "http://www.muratbaseren.com.tr";
            SiteConfig.Obj.ContactErrorMessage = "Üzgünüz, hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            SiteConfig.Obj.ContactSuccessMessage = "Teşekkürler.. En kısa sürede tarafınıza geri dönüş yapılacaktır.";
            SiteConfig.Obj.OrderReceiveMessage = "Siparişiniz alınmıştır. Siparişiniz ile ilgili sizinle telefon/e-posta ile iletişime geçilecektir. Sipariş durum güncellemelerinde tarafınıza ilgili e-posta mesajı gönderilecektir. Lütfen takip ediniz.";
            SiteConfig.Obj.OrderStatusChangedMessage = "Siparişinizin durumu güncellemiştir. Sipariş durumu ve şef'den mesajınız varsa aşağıda görebilirsiniz. Lütfen takipte kalınız.";
            SiteConfig.Obj.OrderReceiveMessageTitle = "En kısa sürede tarafınıza dönüş yapılacaktır.";
            SiteConfig.Obj.OrderStatusChangedMessageTitle = "Sipariş Durumu Değişti";
            SiteConfig.Obj.Description = "Mühendis Aşçı; pasta, kurabiye, kek siparişleri alınan ve alınan siparişlerin tanıtımlarının yapıldığı bir internet sitesidir.";
            SiteConfig.Obj.District1Title = "Adresim";
            SiteConfig.Obj.District2Title = "Sosyal Medya'da MA";
            SiteConfig.Obj.District3Title = "Hakkımda";
            SiteConfig.Obj.Keywords = "mühendis aşçı, pasta, kurabiye, pop cake, sipariş";

            SiteConfig.Obj.SocialItems.Clear();
            SiteConfig.Obj.SocialItems.AddRange(new SocialItem[] {
                new SocialItem() { Id=Guid.NewGuid(), IconName ="facebook", Title = "Facebook'da Mühendis Aşçı", Url="http://www.facebook.com" },
                new SocialItem() { Id=Guid.NewGuid(), IconName ="twitter", Title = "Twitter'da Mühendis Aşçı", Url="http://www.twitter.com" },
                new SocialItem() { Id=Guid.NewGuid(), IconName ="instagram", Title = "Instagram'da Mühendis Aşçı", Url="http://www.instagram.com" },
            });

            SiteConfig.SaveConfig();
        }
    }
}
