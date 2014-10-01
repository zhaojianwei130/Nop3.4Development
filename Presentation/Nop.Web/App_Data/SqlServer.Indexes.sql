CREATE NONCLUSTERED INDEX [IX_LocaleStringResource] ON [LocaleStringResource] ([ResourceName] ASC,  [LanguageId] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_Country_DisplayOrder] ON [Country] ([DisplayOrder] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_StateProvince_CountryId] ON [StateProvince] ([CountryId]) INCLUDE ([DisplayOrder])
GO


CREATE NONCLUSTERED INDEX [IX_Log_CreatedOnUtc] ON [Log] ([CreatedOnUtc] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_Customer_Email] ON [Customer] ([Email] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_Customer_Username] ON [Customer] ([Username] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_Customer_CustomerGuid] ON [Customer] ([CustomerGuid] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_GenericAttribute_EntityId_and_KeyGroup] ON [GenericAttribute] ([EntityId] ASC, [KeyGroup] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_QueuedEmail_CreatedOnUtc] ON [QueuedEmail] ([CreatedOnUtc] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_Language_DisplayOrder] ON [Language] ([DisplayOrder] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_News_LanguageId] ON [News] ([LanguageId] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_NewsComment_NewsItemId] ON [NewsComment] ([NewsItemId] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_PollAnswer_PollId] ON [PollAnswer] ([PollId] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_ActivityLog_CreatedOnUtc] ON [ActivityLog] ([CreatedOnUtc] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_UrlRecord_Slug] ON [UrlRecord] ([Slug] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_UrlRecord_Custom_1] ON [UrlRecord] ([EntityId] ASC, [EntityName] ASC, [LanguageId] ASC, [IsActive] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_AclRecord_EntityId_EntityName] ON [AclRecord] ([EntityId] ASC, [EntityName] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_StoreMapping_EntityId_EntityName] ON [StoreMapping] ([EntityId] ASC, [EntityName] ASC)
GO