# Profile

## Update my Profile

**URL:** `/api/Profile`

**Method:** POST

**Auth required:** Yes

**Body:**

```json
{
  "firstname": "String",
  "lastname": "String",
  "organisation": "String",
  "organisationRole": "String",
  "shortBio": "String",
  "avatarImageId": "String",
  "avatarImageLink": "String",
  "category": "[String]"
}
```

**Response:**

200 Success

```json
{
  "firstname": "String",
  "lastname": "String",
  "organisation": "String",
  "organisationRole": "String",
  "shortBio": "String",
  "avatarImageId": "String",
  "avatarImageLink": "String",
  "category": "[String]",
  "verificationStatus": "String"
}
```

## Submit my Profile verification

**URL:** `/api/Profile/verification`

**Method:** POST

**Auth required:** Yes

**Body:**

```json
{
  "organisation": "String",
  "organisationRole": "String",
  "shortBio": "String",
  "category": "[String]",
  "documentCvId": "String",
  "documentCvUrl": "String",
  "linkedinUrl": "String"

}
```

**Response:**

200 Success

```json
{
  "firstname": "String",
  "lastname": "String",
  "organisation": "String",
  "organisationRole": "String",
  "shortBio": "String",
  "avatarImageId": "String",
  "avatarImageLink": "String",
  "category": "[String]",
  "verificationStatus": "String"
}
```

## Update preferred Categories in my Profile

**URL:** `/api/Profile/updatePreferredCategories`

**Method:** POST

**Auth required:** Yes

**Body:**

```json
{
  "category": "[String]"
}
```

**Response:**

200 Success

```json
{
  "firstname": "String",
  "lastname": "String",
  "organisation": "String",
  "organisationRole": "String",
  "shortBio": "String",
  "avatarImageId": "String",
  "avatarImageLink": "String",
  "category": "[String]",
  "verificationStatus": "String"
}
```
