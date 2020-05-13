using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proj1_OnlineStore.Data
{
	public static class SeedData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new OnlineStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<OnlineStoreDbContext>>()))
			{

				#region Seed Locations
				//Look for any Locations
				if (context.Locations.Any())
				{
					//Locations has been seeded
				}
				else
				{
					context.Locations.AddRange(
					new Models.Location
					{
						LocationName = "Provo"
					},
					new Models.Location
					{
						LocationName = "Salt Lake City"
					},
					new Models.Location
					{
						LocationName = "San Francisco"
					},
					new Models.Location
					{
						LocationName = "Bogo, Philippines"
					},
					new Models.Location
					{
						LocationName = "Manila, Philippines"
					}
					);
				}
				#endregion

				context.SaveChanges();

				#region Seed Products
				if (context.Products.Any())
				{
					//Products already seeded
				}
				else
				{
					
					var bogo = context.Locations.Where(l => l.LocationName == "Bogo, Philippines").FirstOrDefault();
					var provo = context.Locations.Where(l => l.LocationName == "Provo").FirstOrDefault();
					var slc = context.Locations.Where(l => l.LocationName == "Salt Lake City").FirstOrDefault();
					var sf = context.Locations.Where(l => l.LocationName == "San Francisco").FirstOrDefault();
					var manila = context.Locations.Where(l => l.LocationName == "Manila, Philippines").FirstOrDefault();


					context.Products.AddRange(
						#region Provo

						new Models.Product
						{
							LocationId = provo.LocationId,
							ProductName = "Pillow case: Shamrock",
							Qty = 20,
							UnitPrice = 15.00M
						},
						new Models.Product
						{
							LocationId = provo.LocationId,
							ProductName = "Pillow case: Easter",
							Qty = 15,
							UnitPrice = 15.00M
						},
						new Models.Product
						{
							LocationId = provo.LocationId,
							ProductName = "Pillow case: Hearts",
							Qty = 15,
							UnitPrice = 15.00M
						},
						new Models.Product
						{
							LocationId = provo.LocationId,
							ProductName = "Scarf: Butterfly",
							Qty = 15,
							UnitPrice = 10.00M
						},
						new Models.Product
						{
							LocationId = provo.LocationId,
							ProductName = "Tulip: Red",
							Qty = 15,
							UnitPrice = 20.00M
						},

						#endregion

						#region Bogo
						new Models.Product
						{
							LocationId =bogo.LocationId,
							ProductName = "Pillow case: Shamrock",
							Qty = 20,
							UnitPrice = 15.00M
						},
						new Models.Product
						{
							LocationId =bogo.LocationId,
							ProductName = "Pillow case: Hearts",
							Qty = 20,
							UnitPrice = 15.00M
						},
						new Models.Product
						{
							LocationId =bogo.LocationId,
							ProductName = "Tulip: Red",
							Qty = 20,
							UnitPrice = 20.00M
						},
						new Models.Product
						{
							LocationId = bogo.LocationId,
							ProductName = "Tulip: Blue",
							Qty = 20,
							UnitPrice = 20.00M
						},
						new Models.Product
						{
							LocationId =bogo.LocationId,
							ProductName = "Tulip: Purple",
							Qty = 20,
							UnitPrice = 20.00M
						},
						new Models.Product
						{
							LocationId =bogo.LocationId,
							ProductName = "Tulip: White",
							Qty = 20,
							UnitPrice = 20.00M
						},

						#endregion

						#region SLC
						new Models.Product
						{
							LocationId = slc.LocationId,
							ProductName = "Pillow Case: Shamrock",
							Qty = 20,
							UnitPrice = 20.00M
						},
						new Models.Product
						{
							LocationId = slc.LocationId,
							ProductName = "Pillow Case: Easter",
							Qty = 20,
							UnitPrice = 20.00M
						},
						new Models.Product
						{
							LocationId = slc.LocationId,
							ProductName = "Pillow Case: Hearts",
							Qty = 20,
							UnitPrice = 20.00M
						},
						new Models.Product
						{
							LocationId = slc.LocationId,
							ProductName = "Tulip: Purple",
							Qty = 20,
							UnitPrice = 20.00M
						},
						new Models.Product
						{
							LocationId = slc.LocationId,
							ProductName = "Tulip: White",
							Qty = 20,
							UnitPrice = 20.00M
						},

					#endregion

						#region SF
							new Models.Product
							{
								LocationId = sf.LocationId,
								ProductName = "Scarf: Butterfly",
								Qty = 20,
								UnitPrice = 20.00M
							},
							new Models.Product
							{
								LocationId = sf.LocationId,
								ProductName = "Scarf: Leopard",
								Qty = 20,
								UnitPrice = 20.00M
							},
							new Models.Product
							{
								LocationId = sf.LocationId,
								ProductName = "Tulip: Red",
								Qty = 20,
								UnitPrice = 20.00M
							},
							new Models.Product
							{
								LocationId = sf.LocationId,
								ProductName = "Tulip: White",
								Qty = 20,
								UnitPrice = 20.00M
							},

					#endregion

						#region Manila
							new Models.Product
							{
								LocationId = manila.LocationId,
								ProductName = "Pillow Case: Shamrock",
								Qty = 20,
								UnitPrice = 20.00M
							},
							new Models.Product
							{
								LocationId = manila.LocationId,
								ProductName = "Tulip: Red",
								Qty = 20,
								UnitPrice = 20.00M
							},
							new Models.Product
							{
								LocationId = manila.LocationId,
								ProductName = "Tulip: Blue",
								Qty = 20,
								UnitPrice = 20.00M
							},
							new Models.Product
							{
								LocationId = manila.LocationId,
								ProductName = "Tulip: White",
								Qty = 20,
								UnitPrice = 20.00M
							}

					#endregion

						); 
				}
				#endregion

				context.SaveChanges();

				#region Seed Customer
				if(context.Customers.Any())
				{
					//  Customers seeded
				}
				else
				{
					context.Customers.AddRange(
						new Models.Customer
						{
							FirstName = "Bat",
							LastName = "Man",
							Login = "theBMan",
							Password = "bat1234",
							PhoneNumber = "123-456-7890"
						},
						new Models.Customer
						{
							FirstName = "Count",
							LastName = "Dracula",
							Login = "CountD",
							Password = "drak1234",
							PhoneNumber = "098-897-9075"
						}

						);		
				}
				#endregion

				context.SaveChanges();
			}
		}
	}
}
