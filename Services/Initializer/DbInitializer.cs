using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel.Products;
using Interfaces.Initializer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Services.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        public async Task SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    if (!context.Roles.Any())
                    {
                        var roles = new List<IdentityRole>
                        {
                            new IdentityRole {Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = Guid.NewGuid().ToString()}
                        };
                        foreach (var role in roles)
                            context.Roles.Add(role);
                    }

                    var user = new IdentityUser
                    {
                        Email = "admin@gmail.com",
                        NormalizedEmail = "admin@gmail.com".ToUpper(),
                        UserName = "admin@gmail.com",
                        NormalizedUserName = "admin@gmail.com".ToUpper(),
                        SecurityStamp = Guid.NewGuid().ToString("D")
                    };

                    if (!context.Users.Any(u => u.UserName == user.UserName))
                    {
                        var password = new PasswordHasher<IdentityUser>();
                        var hashed = password.HashPassword(user, "123456");
                        user.PasswordHash = hashed;
                        var userStore = new UserStore<IdentityUser>(context);
                        await userStore.CreateAsync(user);
                        await userStore.AddToRoleAsync(user, "ADMIN");
                    }

                    if (!context.ProductCategories.Any())
                    {
                        var productCategories = new List<ProductCategory>
                        {
                            new ProductCategory
                            {
                                Name = "Scaune",
                                Created = DateTime.UtcNow,
                                Products =
                                {
                                    new Product
                                    {
                                        Name = "Scaun Margonia - Albastru de cerneală",
                                        Created = DateTime.UtcNow,
                                        Price = 199,
                                        Manufacturer = "Italia",
                                        Photos =
                                        {
                                            new Photo {Path = "\\assets\\img\\margonia-blue2-web.jpg", Created = DateTime.UtcNow},
                                            new Photo {Path = "\\assets\\img\\margonia--web.jpg", Created = DateTime.UtcNow}
                                        }
                                    },
                                    new Product
                                    {
                                        Name = "Scaun Bellucci - Taupe",
                                        Created = DateTime.UtcNow,
                                        Price = 219,
                                        Manufacturer = "Italia",
                                        Photos =
                                        {
                                            new Photo
                                            {
                                                Path = "\\assets\\img\\j-1167-belluci-taupe-chrome-back-cutout-web.jpg",
                                                Created = DateTime.UtcNow
                                            },
                                            new Photo
                                            {
                                                Path = "\\assets\\img\\j-1167-belluci-taupe-chrome-cutout-web.jpg", Created = DateTime.UtcNow
                                            }
                                        }
                                    }
                                }
                            },
                            new ProductCategory
                            {
                                Name = "Paturi",
                                Created = DateTime.UtcNow,
                                Products =
                                {
                                    new Product
                                    {
                                        Name = "Pat cu oglindă Antoinette - Taupe",
                                        Created = DateTime.UtcNow,
                                        Price = 799,
                                        Manufacturer = "Marea Britanie",
                                        Photos =
                                        {
                                            new Photo {Path = "\\assets\\img\\antoinettebed-crop-web.jpg", Created = DateTime.UtcNow}
                                        }
                                    }
                                }
                            },
                            new ProductCategory
                            {
                                Name = "Lumini de perete",
                                Created = DateTime.UtcNow,
                                Products =
                                {
                                    new Product
                                    {
                                        Name = "Aplica de perete din cristal Meyer",
                                        Created = DateTime.UtcNow,
                                        Price = 79,
                                        Manufacturer = "Germania",
                                        Photos =
                                        {
                                            new Photo {Path = "\\assets\\img\\meyer-wall-light-off-web_3.jpg", Created = DateTime.UtcNow},
                                            new Photo {Path = "\\assets\\img\\meyer-wall-light-on-web_3.jpg", Created = DateTime.UtcNow}
                                        }
                                    },
                                    new Product
                                    {
                                        Name = "Aplica dublă Regis Dome",
                                        Created = DateTime.UtcNow,
                                        Price = 59,
                                        Manufacturer = "Suedia",
                                        Photos =
                                        {
                                            new Photo {Path = "\\assets\\img\\regisdet1-web.jpg", Created = DateTime.UtcNow},
                                            new Photo {Path = "\\assets\\img\\regisdet2-web.jpg", Created = DateTime.UtcNow},
                                            new Photo {Path = "\\assets\\img\\regisnew1-web.jpg", Created = DateTime.UtcNow}
                                        }
                                    }
                                }
                            }
                        };
                        foreach (var productCategory in productCategories)
                            context.ProductCategories.Add(productCategory);
                    }

                    await context.SaveChangesAsync();
                }
            }
        }
    }
}