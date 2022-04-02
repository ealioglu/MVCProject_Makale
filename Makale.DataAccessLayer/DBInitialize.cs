using Makale.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale.DataAccessLayer
{
    public class DBInitialize: CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            //Kullanıcı Ekle
            Kullanici admin = new Kullanici() { 
            Name="Elif",
            Surname="Cengiz",
            Email="cenelif@gmail.com",
            ActivateGuid=Guid.NewGuid(),
            IsActive=true,
            IsAdmin=true,
            Username="elifcengiz",
            Password="1234",
            ProfileImageFile= "user_image.png",
            CreateDate =DateTime.Now,  
            ModifiedDate=DateTime.Now.AddHours(1),
            ModifiedUsername="elifcengiz"           
            };

            context.Kullanicilar.Add(admin);

            for (int i = 0; i < 5; i++)
            {
                Kullanici user = new Kullanici()
                { 
                  Name=FakeData.NameData.GetFirstName(),
                  Surname=FakeData.NameData.GetSurname(),
                  Email=FakeData.NetworkData.GetEmail(),
                  ActivateGuid=Guid.NewGuid(),
                  IsActive=true,
                  IsAdmin=false,
                  Username="user_"+ FakeData.NameData.GetFirstName(),
                  Password="1234",
                  ProfileImageFile = "user_image.png",
                    CreateDate =DateTime.Now.AddDays(-1),
        ModifiedDate=DateTime.Now,
        ModifiedUsername= "user_" + FakeData.NameData.GetFirstName()            

                };

                context.Kullanicilar.Add(user);
            }

            context.SaveChanges();//Kullanıcılar database kaydedildi.

            List<Kullanici> kullanicilar = new List<Kullanici>();  
            kullanicilar=context.Kullanicilar.ToList();

            //Kategori ekle
            for (int i = 0; i < 5; i++)
            {
                Kategori kategori = new Kategori()
                { 
                    Title=FakeData.PlaceData.GetStreetName(),
                    Description=FakeData.PlaceData.GetAddress(),
                    CreateDate=DateTime.Now,
                    ModifiedDate=DateTime.Now,
                    ModifiedUsername= "elifcengiz"
                };
                context.Kategoriler.Add(kategori);

                //Kategoriye not ekle
                for (int j = 0; j < 5; j++)
                {
                    Note note = new Note()
                    { 
                        Title=FakeData.TextData.GetAlphabetical(20),
                        Text=FakeData.TextData.GetSentences(3),
                        IsDraft=false,
                        LikeCount=FakeData.NumberData.GetNumber(1,5),
                        CreateDate=DateTime.Now.AddDays(-2),
                        ModifiedDate=DateTime.Now,
                        Kullanici= kullanicilar[j],
                        ModifiedUsername = kullanicilar[j].Username
                    };

                    kategori.Notes.Add(note);

                    //Nota yorum ekle
                    for (int k = 0; k < 3; k++)
                    {
                        Comment comment = new Comment()
                        {  
                            Text=FakeData.TextData.GetSentence(),
                            Kullanici=kullanicilar[k],
                            CreateDate=DateTime.Now.AddDays(-2),
                            ModifiedDate=DateTime.Now,
                            ModifiedUsername= kullanicilar[k].Username
                        };

                        note.Comments.Add(comment);

                    }

                    //Nota like ekle
                    for (int m = 0; m < 3; m++)
                    {
                        Liked liked = new Liked()
                        { 
                           Kullanici=kullanicilar[m]
                        };

                        note.Likes.Add(liked);
                    }

                }        
            }

            context.SaveChanges();
        

        }
    }
}
