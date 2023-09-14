# Media

We implemented API handling file upload Sitefinity:
- Image
- Video
- Audio
- Document

## Upload new Image

**URL:** `/api/Image/`

**Method:** POST

**Auth required:** Yes

**Body:**

```
    file: "File"
    folder: "Categories | ProfileImages"
```

**Response:**

200 Success

```json
{
  "Url": "String",
  "Width": "Number",
  "Height": "Number",
  "AlternativeText": "String",
  "ParentId": "String",
  "MimeType": "String"
}
```

## Upload new Video

**URL:** `/api/Video/`

**Method:** POST

**Auth required:** Yes

**Body:**

```
    file: "File"
```

**Response:**

200 Success

```json
{
  "Url": "String",
  "Width": "Number",
  "Height": "Number",
  "ParentId": "String",
  "MimeType": "String"
}
```

## Upload new Audio

**URL:** `/api/Audio/`

**Method:** POST

**Auth required:** Yes

**Body:**

```
    file: "File"
    folder: "Categories | ProfileImages"
```

**Response:**

200 Success

```json
{
  "Url": "String",
  "AlternativeText": "String",
  "ParentId": "String",
  "MimeType": "String"
}
```

## Upload new Document

**URL:** `/api/Document/`

**Method:** POST

**Auth required:** Yes

**Body:**

```
    file: "File"
    folder: "presentation"
```

**Response:**

200 Success

```json
{
  "Url": "String",
  "ParentId": "String",
  "MimeType": "String"
}
```
