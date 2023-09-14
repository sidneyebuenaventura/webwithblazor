# BlogPost

## Create new BlogPost

**URL:** `/api/BlogPost`

**Method:** POST

**Auth required:** Yes

**Body:**

```json
{
  "Title": "String",
  "Content": "String",
  "ShorTitle": "String",
  "Category": "[String]",
  "Tags": [
    {
      "Id": "String",
      "Title": "String"
    }
  ],
  "IsDraft": "Boolean",
  "CoverImageUpload": "File"
}
```

**Response:**

200 Success

```json
{
  "BlogPost": {
    "Id": "String",
    "Title": "String",
    "Content": "String",
    "ShorTitle": "String",
    "Category": "[String]",
    "Tags": [
      {
        "Id": "String",
        "Title": "String"
      }
    ],
    "PublishingStatus": "String",
    "ItemDefaultUrl": "String"
  },
  "CreatedTags": [
    {
      "Id": "String",
      "Title": "String"
    }
  ],
  "CoverImage": {
    "Url": "String",
    "Width": "Number",
    "Height": "Number",
    "AlternativeText": "String"
  }
}
```

## Update a BlogPost

**URL:** `/api/BlogPost`

**Method:** PUT

**Auth required:** Yes

**Permissions:** BlogPost can only be updated by its Owner

**Body:**

```json
{
  "Id": "String",
  "Title": "String",
  "Content": "String",
  "ShorTitle": "String",
  "Category": "[String]",
  "Tags": [
    {
      "Id": "String",
      "Title": "String"
    }
  ],
  "IsDraft": "Boolean",
  "CoverImageUpload": "File",
  "NewCoverImage": "Boolean"
}
```

**Response:**

200 Success

```json
{
  "BlogPost": {
    "Id": "String",
    "Title": "String",
    "Content": "String",
    "ShorTitle": "String",
    "Category": "[String]",
    "Tags": [
      {
        "Id": "String",
        "Title": "String"
      }
    ],
    "PublishingStatus": "String",
    "ItemDefaultUrl": "String"
  },
  "CreatedTags": [
    {
      "Id": "String",
      "Title": "String"
    }
  ],
  "CoverImage": {
    "Url": "String",
    "Width": "Number",
    "Height": "Number",
    "AlternativeText": "String"
  }
}
```

## Delete a BlogPost

**URL:** `/api/BlogPost?id=[PostId]`

**Method:** DELETE

**Auth required:** Yes

**Permissions:** BlogPost can only be deleted by its Owner

**Query params:**

```
"id": "String"
```

**Response:**

200 Success

## Search for BlogPosts

**URL:** `/api/BlogPost?searchText=[SearchText]&excludedPostIds=[ExcludedPostIds]`

**Method:** GET

**Auth required:** Yes

**Query params:**

```
"searchText": "String"
"excludedPostIds": "String,...,String"
```

**Response:**

200 Success: Partial view [PostViewModelList.cshtml](../Views/Shared/DisplayTemplates/PostViewModelList.cshtml)

## Like / Unlike a BlogPost

**URL:** `/api/BlogPost/LikeUnlike/[PostId]`

**Method:** PUT

**Auth required:** Yes

**Path params:**

```
"PostId": "String"
```

**Response:**

200 Success

## See more BlogPosts in Category

**URL:** `/api/BlogPost/posts?categoryId=[CategoryId]&count=[Count]&excludedPostIds=[ExcludedPostIds]`

**Method:** GET

**Auth required:** No

**Query params:**

```
"categoryId": "String"
"count": "Number"
"excludedPostIds": "String,...,String"
```

**Response:**

200 Success: Partial view [PostViewModelList.cshtml](../Views/Shared/DisplayTemplates/PostViewModelList.cshtml)

## See more BlogPosts in "For you" page

**URL:** `/api/BlogPost/postsForYou?count=[Count]&excludedPostIds=[ExcludedPostIds]`

**Method:** GET

**Auth required:** No

**Query params:**

```
"count": "Number"
"excludedPostIds": "String,...,String"
```

**Response:**

200 Success: Partial view [PostViewModelList.cshtml](../Views/Shared/DisplayTemplates/PostViewModelList.cshtml)