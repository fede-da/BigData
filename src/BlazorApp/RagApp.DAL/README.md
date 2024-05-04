# Migrations

Create migrations using Asp .Net Core CLI, launch the command from DAL project folder:

``` dotnet ef --startup-project ..\RagApp\RagApp\ migrations add <MigrationName> ```

Update database using Asp .Net Core CLI

``` dotnet ef database update --project . --startup-project ..\RagApp\RagApp\ ```

# Postgres vs Mongo

Short discussion about their differences: Structured vs unstructured data

## Postgres

In an e-commerce order system, you typically have structured and interrelated data. This includes customers, orders, items, and payments. The relationships among these entities are clearly defined, which makes a relational database ideal due to its ability to maintain these relationships through foreign keys and join operations.

```sql
CREATE TABLE Customers (
    CustomerID int PRIMARY KEY,
    Name varchar(255),
    Email varchar(255),
    Address varchar(255)
);
```

## Mongo

In a social media application, you deal with semi-structured or unstructured data that includes posts, user profiles, comments, and media attachments. The data can be nested and does not require complex joins.

```json
{
  "_id": ObjectId("abcd1234"),
  "username": "user1",
  "email": "user1@example.com",
  "profile_picture": "url_to_image",
  "posts": [
    {
      "post_id": ObjectId("post123"),
      "text": "Exploring NoSQL databases!",
      "images": ["img1_url", "img2_url"],
      "comments": [
        {
          "username": "commenter1",
          "text": "Very interesting!",
          "comment_date": ISODate("2022-01-01T08:00:00Z")
        }
      ],
      "post_date": ISODate("2022-01-01T07:45:00Z")
    }
  ]
}
```
