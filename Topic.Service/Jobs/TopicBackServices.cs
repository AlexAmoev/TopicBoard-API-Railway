using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topic.Contracts;
using Topic.Data;
using Topic.Entities;

namespace Topic.BackService.Jobs
{
    public class TopicBackServices : BackgroundService
    {
        private readonly ILogger<TopicBackServices> _logger;
        private readonly IServiceProvider _serviceProvider;

        public TopicBackServices(ILogger<TopicBackServices> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                        var comments = await dbContext.Comments.ToListAsync(stoppingToken);
                        var topics = await dbContext.Topics.ToListAsync(stoppingToken);


                        for (int i = 0; i < topics.Count; i++)
                        {
                            for (int j = 0; j < comments.Count; j++)
                            {
                                if (topics[i].Id == comments[j].TopicEntityId)
                                {
                                    DateTime currentDate = DateTime.Now;
                                    TimeSpan timeDifferenceCommentPostDate = currentDate - comments[j].PostedDate;
                                    TimeSpan timeDifferenceTopicPostDate = currentDate - topics[i].StartDate;
                                    if (timeDifferenceCommentPostDate > TimeSpan.FromDays(3))
                                    {
                                        topics[i].Status = Status.Inactive;
                                        //_logger.LogInformation($"Topic {topics[i].Title} has been deactivated !\nTopic ID: {topics[i].Id}");
                                        await dbContext.SaveChangesAsync();
                                    }
                                    else if (topics[i].CommentsCount == 0 && timeDifferenceTopicPostDate > TimeSpan.FromDays(3))
                                    {
                                        topics[i].Status = Status.Inactive;
                                        //_logger.LogInformation($"Topic {topics[i].Title} has been deactivated !\nTopic ID: {topics[i].Id}");
                                        await dbContext.SaveChangesAsync();
                                    }
                                }
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ("ERROR WHILE TOPIC BACKGROUND JOB EXECUTION"));
                }
            }
        }
    }
}
