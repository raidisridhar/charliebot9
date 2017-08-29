namespace LuisBot.Dialogs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.FormFlow;
    using Microsoft.Bot.Builder.Luis;
    using Microsoft.Bot.Builder.Luis.Models;
    using Microsoft.Bot.Connector;
  

    [LuisModel("87001b9b-2bb7-461f-af7e-3dc08032f9e4", "9660d9c0c855475eace4e50e5e69d61c")]
    [Serializable]
    public class RootLuisDialog : LuisDialog<object>
    {
        private const string EntityGeographyCity = "builtin.geography.city";

        private const string EntityHotelName = "Hotel";

        private const string EntityAirportCode = "AirportCode";

        private const string EntityClinicalTrail = "Clinical trail";

        private const string EntityCancer = "Cancer";

        private const string EntityTest = "Test";
        
        private IList<string> titleOptions = new List<string> { "“EGFR”", "“JAK2”", "“KRAS”", "“BRAF”", "“NRAS”", "“Tumor percent”" };


        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Sorry, I did not understand '{result.Query}'. Type 'help' if you need assistance.";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Checklist")]
        public async Task Checklist(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            await context.PostAsync("These are the tasks for visit 2");
            await context.PostAsync("Take vitals and temperature");
            await context.PostAsync("Grade the disease");
            await context.PostAsync("Etc...");

            context.Wait(this.MessageReceived);

            //var message = await activity;
            //await context.PostAsync($"Welcome! We are analyzing your message: '{message.Text}'...");

            //var hotelsQuery = new HotelsQuery();

            //EntityRecommendation cityEntityRecommendation;

            //if (result.TryFindEntity(EntityGeographyCity, out cityEntityRecommendation))
            //{
            //    cityEntityRecommendation.Type = "Destination";
            //}

            //var hotelsFormDialog = new FormDialog<HotelsQuery>(hotelsQuery, this.BuildHotelsForm, FormOptions.PromptInStart, result.Entities);

            //context.Call(hotelsFormDialog, this.ResumeAfterHotelsFormDialog);
        }

        [LuisIntent("Trial")]
        public async Task Trial(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("The trial end points are...");
            await context.PostAsync("Disease free survival");
            await context.PostAsync("Decrease in tumor percent");
            await context.PostAsync("Normal function of body vitals");

            context.Wait(this.MessageReceived);

            //EntityRecommendation hotelEntityRecommendation;

            //if (result.TryFindEntity(EntityHotelName, out hotelEntityRecommendation))
            //{
            //    await context.PostAsync($"Looking for reviews of '{hotelEntityRecommendation.Entity}'...");

            //    var resultMessage = context.MakeMessage();
            //    resultMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            //    resultMessage.Attachments = new List<Attachment>();

            //    for (int i = 0; i < 5; i++)
            //    {
            //        var random = new Random(i);
            //        ThumbnailCard thumbnailCard = new ThumbnailCard()
            //        {
            //            Title = this.titleOptions[random.Next(0, this.titleOptions.Count - 1)],
            //            Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris odio magna, sodales vel ligula sit amet, vulputate vehicula velit. Nulla quis consectetur neque, sed commodo metus.",
            //            Images = new List<CardImage>()
            //            {
            //                new CardImage() { Url = "https://upload.wikimedia.org/wikipedia/en/e/ee/Unknown-person.gif" }
            //            },
            //        };

            //        resultMessage.Attachments.Add(thumbnailCard.ToAttachment());
            //    }

            //    await context.PostAsync(resultMessage);
            //}

            //context.Wait(this.MessageReceived);
        }
        [LuisIntent("Gleasonscore")]
        public async Task Gleasonscore(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("A system of grading prostate cancer tissue based on how it looks under a microscope. Gleason scores range from 2 to 10 and indicate how likely it is that a tumor will spread. A low Gleason score means the cancer tissue is similar to normal prostate tissue and the tumor is less likely to spread; a high Gleason score means the cancer tissue is very different from normal and the tumor is more likely to spread.");

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("EGFR")]
        public async Task EGFR(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("The estimated glomerular filtration rate (eGFR) is used to screen for and detect early kidney damage, to help diagnose chronic kidney disease (CKD), and to monitor kidney status");

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Mammaprint")]
        public async Task Mammaprint(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("MammaPrint test is a genomic test that analyzes the activity of certain genes in early-stage breast cancer. Research suggests the MammaPrint test may eventually be widely used to help make treatment decisions based on the cancer's risk of coming back (recurrence) within 10 years after diagnosis.");

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Cancerstaging")]
        public async Task Cancerstaging(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Cancer staging is the process of determining how much cancer is in the body and where it is located. Staging describes the severity of an individual's cancer based on the magnitude of the original (primary) tumor as well as on the extent cancer has spread in the body.");

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("ReportedOutcome")]
        public async Task ReportedOutcome(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("It is SF 36");

            context.Wait(this.MessageReceived);
        }


        [LuisIntent("Help")]
        public async Task Help(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Hi! Try asking me things like 'what are the trail end points', 'checklist for visit 2',  'what is the term gleason score mean', 'Cancer staging according to NCI', 'What are the patient reported outcomes for visit 2'");

            context.Wait(this.MessageReceived);
        }

        private IForm<HotelsQuery> BuildHotelsForm()
        {
            OnCompletionAsyncDelegate<HotelsQuery> processHotelsSearch = async (context, state) =>
            {
                var message = "Searching for hotels";
                if (!string.IsNullOrEmpty(state.Destination))
                {
                    message += $" in {state.Destination}...";
                }
                else if (!string.IsNullOrEmpty(state.AirportCode))
                {
                    message += $" near {state.AirportCode.ToUpperInvariant()} airport...";
                }

                await context.PostAsync(message);
            };

            return new FormBuilder<HotelsQuery>()
                .Field(nameof(HotelsQuery.Destination), (state) => string.IsNullOrEmpty(state.AirportCode))
                .Field(nameof(HotelsQuery.AirportCode), (state) => string.IsNullOrEmpty(state.Destination))
                .OnCompletion(processHotelsSearch)
                .Build();
        }

        private async Task ResumeAfterHotelsFormDialog(IDialogContext context, IAwaitable<HotelsQuery> result)
        {
            try
            {
                var searchQuery = await result;

                var hotels = await this.GetHotelsAsync(searchQuery);

                await context.PostAsync($"I found {hotels.Count()} hotels:");

                var resultMessage = context.MakeMessage();
                resultMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                resultMessage.Attachments = new List<Attachment>();

                foreach (var hotel in hotels)
                {
                    HeroCard heroCard = new HeroCard()
                    {
                        Title = hotel.Name,
                        Subtitle = $"{hotel.Rating} starts. {hotel.NumberOfReviews} reviews. From ${hotel.PriceStarting} per night.",
                        Images = new List<CardImage>()
                        {
                            new CardImage() { Url = hotel.Image }
                        },
                        Buttons = new List<CardAction>()
                        {
                            new CardAction()
                            {
                                Title = "More details",
                                Type = ActionTypes.OpenUrl,
                                Value = $"https://www.bing.com/search?q=hotels+in+" + HttpUtility.UrlEncode(hotel.Location)
                            }
                        }
                    };

                    resultMessage.Attachments.Add(heroCard.ToAttachment());
                }

                await context.PostAsync(resultMessage);
            }
            catch (FormCanceledException ex)
            {
                string reply;

                if (ex.InnerException == null)
                {
                    reply = "You have canceled the operation.";
                }
                else
                {
                    reply = $"Oops! Something went wrong :( Technical Details: {ex.InnerException.Message}";
                }

                await context.PostAsync(reply);
            }
            finally
            {
                context.Done<object>(null);
            }
        }

        private async Task<IEnumerable<Hotel>> GetHotelsAsync(HotelsQuery searchQuery)
        {
            var hotels = new List<Hotel>();

            // Filling the hotels results manually just for demo purposes
            for (int i = 1; i <= 5; i++)
            {
                var random = new Random(i);
                Hotel hotel = new Hotel()
                {
                    Name = $"{searchQuery.Destination ?? searchQuery.AirportCode} Hotel {i}",
                    Location = searchQuery.Destination ?? searchQuery.AirportCode,
                    Rating = random.Next(1, 5),
                    NumberOfReviews = random.Next(0, 5000),
                    PriceStarting = random.Next(80, 450),
                    Image = $"https://placeholdit.imgix.net/~text?txtsize=35&txt=Hotel+{i}&w=500&h=260"
                };

                hotels.Add(hotel);
            }

            hotels.Sort((h1, h2) => h1.PriceStarting.CompareTo(h2.PriceStarting));

            return hotels;
        }
    }
}
