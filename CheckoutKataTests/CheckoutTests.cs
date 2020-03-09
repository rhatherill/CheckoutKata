using System;
using CheckoutKata;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CheckoutKataTests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void CanScanItem()
		{
			//Arrange
			var checkout = new Checkout();
			Item item = new Item();

			//Act
			checkout.Scan( item );

			//Assert
			Assert.AreEqual( 1, checkout.Items.Count );			
		}

		[TestMethod]
		public void RetrieveTotalPrice()
		{
			//Arrange
			var checkout = new Checkout();
			Item item1 = new Item() { Price = 10.00m };
			Item item2 = new Item() { Price = 11.00m };
			Item item3 = new Item() { Price = 22.10m };

			//Act
			checkout.Scan( item1 );
			checkout.Scan( item2 );
			checkout.Scan( item3 );

			//Assert
			Assert.AreEqual( 43.10m, checkout.Total() );
		}

		[TestMethod]
		public void RetrieveTotalPriceNoItems()
		{
			//Arrange
			var checkout = new Checkout();

			//Act

			//Assert
			Assert.AreEqual( 0m, checkout.Total() );
		}

		[TestMethod]
		public void ConfirmOffersAreApplied()
		{
			//Arrange
			var checkout = new Checkout();

			Offer offerA99 = new Offer() { SKU = "A99", Quantity = 3, OfferPrice = 1.30m };
			Offer offerB15 = new Offer() { SKU = "B15", Quantity = 2, OfferPrice = 0.45m };

			Item a991 = new Item() { SKU = "A99", Price = 0.50m };
			Item a992 = new Item() { SKU = "A99", Price = 0.50m };
			Item a993 = new Item() { SKU = "A99", Price = 0.50m };

			Item b15A = new Item() { SKU = "B15", Price = 0.30m };
			Item b15B = new Item() { SKU = "B15", Price = 0.30m };

			//Act
			checkout.Offers.Add( offerA99 );
			checkout.Offers.Add( offerB15 );

			checkout.Scan( a991 );
			checkout.Scan( a992 );
			checkout.Scan( a993 );

			checkout.Scan( b15A );
			checkout.Scan( b15B );

			//Assert
			Assert.AreEqual( 1.30m + 0.45m, checkout.Total() );
		}

		[TestMethod]
		public void ConfirmMultipleOffersAreApplied()
		{
			//Arrange
			var checkout = new Checkout();

			Offer offer = new Offer() { SKU = "A99", Quantity = 3, OfferPrice = 1.30m };

			Item a991 = new Item() { SKU = "A99", Price = 0.50m };
			Item a992 = new Item() { SKU = "A99", Price = 0.50m };
			Item a993 = new Item() { SKU = "A99", Price = 0.50m };
			Item a994 = new Item() { SKU = "A99", Price = 0.50m };
			Item a995 = new Item() { SKU = "A99", Price = 0.50m };
			Item a996 = new Item() { SKU = "A99", Price = 0.50m };

			//Act
			checkout.Offers.Add( offer );
			checkout.Scan( a991 );
			checkout.Scan( a992 );
			checkout.Scan( a993 );
			checkout.Scan( a994 );
			checkout.Scan( a995 );
			checkout.Scan( a996 );

			//Assert
			Assert.AreEqual( 2.60m, checkout.Total() );
		}
	}
}
