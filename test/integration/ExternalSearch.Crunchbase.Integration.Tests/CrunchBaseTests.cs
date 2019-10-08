using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using CluedIn.Core.Data;
using CluedIn.Core.Data.Parts;
using CluedIn.Core.Messages.Processing;
using CluedIn.Core.Processing;
using CluedIn.Core.Serialization;
using CluedIn.Core.Workflows;
using CluedIn.ExternalSearch;
using CluedIn.ExternalSearch.Providers.Crunchbase;
using CluedIn.Testing.Base.Context;
using CluedIn.Testing.Base.ExternalSearch;
using CluedIn.Testing.Base.Processing.Actors;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace ExternalSearch.Crunchbase.Integration.Tests
{
    public class CrunchBaseTests : BaseExternalSearchTest<CrunchbasePeopleExternalSearchProvider>
    {
        // TODO Issue 170 - Test Failures
        [Fact(Skip = "Test failures")]
        public void Test()
        {
            // Arrange
            this.testContext = new TestContext();
            var properties = new EntityMetadataPart();
            properties.Properties.Add(CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.JobTitle, "CEO");
            properties.Properties.Add(CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.Organization, "Sitecore");

            IEntityMetadata entityMetadata = new EntityMetadataPart()
            {
                Name = "Mark Frost",
                EntityType = EntityType.Person,
                Properties = properties.Properties
            };

            var externalSearchProvider = new Mock<CrunchbasePeopleExternalSearchProvider>(MockBehavior.Loose);
            var clues = new List<CompressedClue>();

            externalSearchProvider.CallBase = true;

            this.testContext.ProcessingHub.Setup(h => h.SendCommand(It.IsAny<ProcessClueCommand>())).Callback<IProcessingCommand>(c => clues.Add(((ProcessClueCommand)c).Clue));

            this.testContext.Container.Register(Component.For<IExternalSearchProvider>().UsingFactoryMethod(() => externalSearchProvider.Object));

            var context = this.testContext.Context.ToProcessingContext();
            var command = new ExternalSearchCommand();
            var actor = new ExternalSearchProcessingAccessor(context.ApplicationContext);
            var workflow = new Mock<Workflow>(MockBehavior.Loose, context, new EmptyWorkflowTemplate<ExternalSearchCommand>());

            workflow.CallBase = true;

            command.With(context);
            command.OrganizationId = context.Organization.Id;
            command.EntityMetaData = entityMetadata;
            command.Workflow = workflow.Object;
            context.Workflow = command.Workflow;

            // Act
            var result = actor.ProcessWorkflowStep(context, command);
            Assert.Equal(WorkflowStepResult.Repeat.SaveResult, result.SaveResult);

            result = actor.ProcessWorkflowStep(context, command);
            Assert.Equal(WorkflowStepResult.Success.SaveResult, result.SaveResult);
            context.Workflow.AddStepResult(result);

            context.Workflow.ProcessStepResult(context, command);

            // Assert
            this.testContext.ProcessingHub.Verify(h => h.SendCommand(It.IsAny<ProcessClueCommand>()), Times.AtLeastOnce);

            Assert.True(clues.Count > 0);
        }

        [Fact(Skip = "Test failures")]
        public void Test2()
        {
            // Arrange
            this.testContext = new TestContext();
            var properties = new EntityMetadataPart();
            properties.Properties.Add(CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Website, "http://cluedin.net");
            properties.Properties.Add("Website", "http://cluedin.net");

            IEntityMetadata entityMetadata = new EntityMetadataPart()
            {
                Name = "CluedIn",
                EntityType = EntityType.Organization,
                Properties = properties.Properties
            };

            var externalSearchProvider = new Mock<CrunchbaseExternalSearchProvider>(MockBehavior.Loose);
            var clues = new List<CompressedClue>();

            externalSearchProvider.CallBase = true;

            this.testContext.ProcessingHub.Setup(h => h.SendCommand(It.IsAny<ProcessClueCommand>())).Callback<IProcessingCommand>(c => clues.Add(((ProcessClueCommand)c).Clue));

            this.testContext.Container.Register(Component.For<IExternalSearchProvider>().UsingFactoryMethod(() => externalSearchProvider.Object));

            var context = this.testContext.Context.ToProcessingContext();
            var command = new ExternalSearchCommand();
            var actor = new ExternalSearchProcessingAccessor(context.ApplicationContext);
            var workflow = new Mock<Workflow>(MockBehavior.Loose, context, new EmptyWorkflowTemplate<ExternalSearchCommand>());

            workflow.CallBase = true;

            command.With(context);
            command.OrganizationId = context.Organization.Id;
            command.EntityMetaData = entityMetadata;
            command.Workflow = workflow.Object;
            context.Workflow = command.Workflow;

            // Act
            var result = actor.ProcessWorkflowStep(context, command);
            Assert.Equal(WorkflowStepResult.Repeat.SaveResult, result.SaveResult);

            result = actor.ProcessWorkflowStep(context, command);
            Assert.Equal(WorkflowStepResult.Success.SaveResult, result.SaveResult);
            context.Workflow.AddStepResult(result);

            context.Workflow.ProcessStepResult(context, command);

            // Assert
            this.testContext.ProcessingHub.Verify(h => h.SendCommand(It.IsAny<ProcessClueCommand>()), Times.AtLeastOnce);

            Assert.True(clues.Count > 0);
        }
    }
}
