﻿using PromotionEngine.Cart;
using PromotionEngine.Sku;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Promotions
{
    public abstract class PromotionRule
    {
        public PromotionRule(string ruleName, List<PromotionProduct> promotionProducts)
        {
            RuleName = ruleName;
            PromotionProducts = promotionProducts;
            IsActive = true;
        }
        /// <summary>
        /// It will be used as rule unique identifier so it must be unique.
        /// </summary>
        public string RuleName { get; set; }
        public string RuleDescription { get; set; }
        public List<PromotionProduct> PromotionProducts { get; set; }
        public bool IsActive {get;set;}

        public bool HasAppliedPromotion { get; set; }
        public virtual void Execute(List<CartItem> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (ValidatePromotionProducts(items))
            {
                ApplyPromotion(items);
            }
        }
        public abstract List<CartItem> ApplyPromotion(List<CartItem> items);

        #region Private Methods
        private bool ValidatePromotionProducts(List<CartItem> items)
        {
            // Check if this rule have Promotion products assigned           
            if (PromotionProducts == null || PromotionProducts.Count == 0)
            {
                return false;
            }

            bool hasAllPromotionProductsToCart = true;
            foreach (var pProduct in PromotionProducts)
            {
                if (!items.Any(i => i.Product.Name.Equals(pProduct.ProductName)))
                {
                    hasAllPromotionProductsToCart = false;
                    break;
                }
            }
            return hasAllPromotionProductsToCart;
        }
        #endregion
    }
}