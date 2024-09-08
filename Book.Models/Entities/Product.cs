﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book.Models.Entities;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }


    [Required]
    public string Author { get; set; }

    [Required]
    [Display(Name = "List Price")]
    [Range(1,1000)]
    public double ListPrice { get; set; }

    [Required]
    [Display(Name = "Price for 1-50")]
    [Range(1, 1000)]
    public double Price { get; set; }

    [Required]
    [Display(Name = "Price for 50+")]
    [Range(1, 1000)]
    public double Price50 { get; set; }

    [Required]
    [Display(Name = "Price for 100+")]
    [Range(1, 1000)]
    public double Price100 { get; set; }
    public string Description { get; set; }
    public int CategoryID { get; set; }
    [ForeignKey("CategoryID")]
    [ValidateNever]
    public Category Category { get; set; }
    [ValidateNever]
    public string ImageUrl { get; set; } 
}
