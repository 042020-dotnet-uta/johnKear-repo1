﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace proj1_OnlineStore.Models
{
	public class ProductsViewModel
	{
		public List<Product> Products { get; set; }
		public SelectList Locations { get; set; }
		public string Location { get; set; }
		public string SearchString { get; set; }
		
		[DataType(DataType.Currency)]
		public double Total { get; set; }
	}
}
