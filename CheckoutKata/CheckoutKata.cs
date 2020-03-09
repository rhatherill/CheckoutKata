using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata
{
	public class Checkout
	{
		/// <summary>
		/// Return the checkout total, with any offers applied
		/// </summary>
		public decimal Total()
		{
			foreach( var offer in Offers )
			{
				ApplyOffer( offer );
			}
			return Items.Sum( p => p.Price );
		}

		/// <summary>
		/// Add an item to the checkout
		/// </summary>
		public void Scan( Item item )
		{
			Items.Add( item );
		}

		/// <summary>
		/// Apply the supplied offer to the checkout item collection
		/// </summary>
		private void ApplyOffer( Offer offer )
		{
			while( GetOfferItems( offer ).Count >= offer.Quantity )
			{
				var remainingOfferItems = GetOfferItems( offer );

				//Set all prices to 0 and let it be known that the offer has been applied
				for( int j = 0; j < offer.Quantity; j++ )
				{
					remainingOfferItems[j].Price = 0m;
					remainingOfferItems[j].OfferApplied = true;
				}

				//Set one item to have the offer price
				remainingOfferItems[0].Price = offer.OfferPrice;
			}
		}

		/// <summary>
		/// Returns items valid for the current offer, which have not had offers applied
		/// </summary>
		private List<Item> GetOfferItems( Offer offer )
		{
			return Items.FindAll( p => p.SKU == offer.SKU && p.OfferApplied == false );
		}

		/// <summary>
		/// The list of offers
		/// </summary>
		public List<Offer> Offers = new List<Offer>();

		/// <summary>
		/// Items in the checkout
		/// </summary>
		public List<Item> Items = new List<Item>();

	}

	/// <summary>
	/// Items that can be scanned and have offers applied
	/// </summary>
	public class Item
	{
		public string SKU;
		public decimal Price;
		public bool OfferApplied;
	}

	/// <summary>
	/// Offers which can be applied to Items
	/// </summary>
	public class Offer
	{
		public string SKU;
		public int Quantity;
		public decimal OfferPrice;
	}

}
