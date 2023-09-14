# Comment

## Create new Comment

**URL:** `/api/Comment`

**Method:** POST

**Auth required:** Yes

**Body:**

```json
{
  "Message": "String",
  "ContentId": "String",
  "Title": "String"
}
```

**Response:**

200 Success: Partial view [PostViewModelList.cshtml](../Views/Shared/DisplayTemplates/CommentViewModel.cshtml)

## Update a Comment

**URL:** `/api/Comment`

**Method:** PUT

**Auth required:** Yes

**Permissions:** Comment can only be updated by its Author

**Body:**

```json
{
  "Message": "String",
  "Key": "String"
}
```

**Response:**

200 Success: Partial view [PostViewModelList.cshtml](../Views/Shared/DisplayTemplates/CommentViewModel.cshtml)

## Delete a Comment

**URL:** `/api/Comment/[CommentKey]`

**Method:** DELETE

**Auth required:** Yes

**Permissions:** BlogPost can only be deleted by its Author

**Path params:**

```
"CommentKey": "String"
```

**Response:**

200 Success