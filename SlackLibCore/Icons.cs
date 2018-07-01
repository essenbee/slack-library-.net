using System;

namespace SlackLibCore
{
	//https://api.slack.com/events/bot_added

	public class Icons
	{
		public String image_48 { get; }

		public Icons(dynamic Data)
		{
			image_48 = Data.images_48;
		}
	}
}
