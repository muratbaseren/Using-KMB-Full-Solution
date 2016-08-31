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
using Using_KMB_Full_Solution.Infrastructure;
using MuhendisAsci.Web.Models.Abstract;

namespace MuhendisAsci.Web.DataAccessLayer
{
    public partial class DatabaseContext : DbContext
    {
        private bool _DoNotModifyTableBaseInfo;

        public DbSet<SysUser> Users { get; set; }
        public DbSet<SysLog> ErrorLogs { get; set; }

        public DatabaseContext()
        {
            Database.SetInitializer<DatabaseContext>(new MyInitializer());
        }

        /// <summary>
        /// Bu metot ile EF üzerinde kaydetme işlemini çağırdığınızda temel EF SaveChanges işleminin gerçekleşmesini, kendi sağladığımız TableBaseInfo verilerinin işlenmesini devre dışı bırakmış olursunuz. Bazı durumlarda bu gerekmektedir.
        /// </summary>
        /// <param name="doNotModifyTableBaseInfo">TableBaseInfo özelliklerini otomatik değiştirilmesi için true aksi halde false olarak veriniz.</param>
        /// <returns></returns>
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
                if (entity.Entity is SysTableBase)
                {
                    SysTableBase entityBase = entity.Entity as SysTableBase;
                    DateTime now = DateTime.Now;

                    SysUser user = null;

                    if (HttpContext.Current.Session[MySessionNames.login] != null)
                    {
                        user = HttpContext.Current.Session[MySessionNames.login] as SysUser;
                    }

                    switch (entity.State)
                    {
                        case EntityState.Added:
                            entityBase.CreatedOn = now;
                            entityBase.CreatedUser = user?.Username ?? MyMagicStrings.noname;
                            break;
                        case EntityState.Deleted:
                            //// TODO : Eğer hiçbir kayıt silinmesin istenirse, ilgili entity'e IIneffaceable interface implement edilmeli ve IsDeleted = true yapılarak entity.state = EntityState.Modified olarak değiştirilmeli.
                            //entityBase.ModifiedOn = now;
                            //entityBase.ModifiedUser = App.CurrentUser?.KullaniciAdi ?? MagicStrings.noname;


                            //// ***************************************************************************
                            //// Silinen kayıtlar günlük olarak yyyyMMdd.xml isimli bir dosyada saklanır.
                            //// Sadece DbTableBase 'den türemiş olan nesneler için bu kayıt yapılır.
                            //// ***************************************************************************
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
                            //// ***************************************************************************
                            //// ***************************************************************************

                            break;
                        case EntityState.Detached:
                            break;
                        case EntityState.Modified:
                            entityBase.ModifiedOn = now;
                            entityBase.ModifiedUser = user?.Username ?? MyMagicStrings.noname;

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
    }

    public partial class MyInitializer:CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            SysUser admin = new SysUser();
            admin.IsActive = true;
            admin.IsDefault = true;
            admin.Username = "admin";
            admin.Password = "123456";
            admin.EMail = "my@mail.com";

            context.Users.Add(admin);
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
            SysSiteConfig.LoadConfig();

            SysSiteConfig.Obj.Title = "Mühendis Aşçı";
            SysSiteConfig.Obj.Author = "K. Murat Başeren";
            SysSiteConfig.Obj.AuthorUrl = "http://www.muratbaseren.com.tr";
            SysSiteConfig.Obj.ContactErrorMessage = "Üzgünüz, hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            SysSiteConfig.Obj.ContactSuccessMessage = "Teşekkürler.. En kısa sürede tarafınıza geri dönüş yapılacaktır.";
            SysSiteConfig.Obj.OrderReceiveMessage = "Siparişiniz alınmıştır. Siparişiniz ile ilgili sizinle telefon/e-posta ile iletişime geçilecektir. Sipariş durum güncellemelerinde tarafınıza ilgili e-posta mesajı gönderilecektir. Lütfen takip ediniz.";
            SysSiteConfig.Obj.OrderStatusChangedMessage = "Siparişinizin durumu güncellemiştir. Sipariş durumu ve şef'den mesajınız varsa aşağıda görebilirsiniz. Lütfen takipte kalınız.";
            SysSiteConfig.Obj.OrderReceiveMessageTitle = "En kısa sürede tarafınıza dönüş yapılacaktır.";
            SysSiteConfig.Obj.OrderStatusChangedMessageTitle = "Sipariş Durumu Değişti";
            SysSiteConfig.Obj.Description = "Mühendis Aşçı; pasta, kurabiye, kek siparişleri alınan ve alınan siparişlerin tanıtımlarının yapıldığı bir internet sitesidir.";
            SysSiteConfig.Obj.District1Title = "Adresim";
            SysSiteConfig.Obj.District2Title = "Sosyal Medya'da MA";
            SysSiteConfig.Obj.District3Title = "Hakkımda";
            SysSiteConfig.Obj.Keywords = "mühendis aşçı, pasta, kurabiye, pop cake, sipariş";

            SysSiteConfig.Obj.SocialItems.Clear();
            SysSiteConfig.Obj.SocialItems.AddRange(new SysSocialItem[] {
                new SysSocialItem() { Id=Guid.NewGuid(), IconName ="facebook", Title = "Facebook'da Mühendis Aşçı", Url="http://www.facebook.com" },
                new SysSocialItem() { Id=Guid.NewGuid(), IconName ="twitter", Title = "Twitter'da Mühendis Aşçı", Url="http://www.twitter.com" },
                new SysSocialItem() { Id=Guid.NewGuid(), IconName ="instagram", Title = "Instagram'da Mühendis Aşçı", Url="http://www.instagram.com" },
            });

            SysSiteConfig.SaveConfig();
        }
    }
}
