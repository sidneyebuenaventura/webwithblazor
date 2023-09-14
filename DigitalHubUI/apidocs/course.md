# Course

We implemented creating / updating Course in combination with creating / updating associated Modules,
Quiz, QuizQuestions, QuizQuestionOptions.

## Create new Course

**URL:** `/api/Course`

**Method:** POST

**Auth required:** Yes

**Body:**

```json
{
  "Title": "String",
  "Subtitle": "String",
  "Category": "String",
  "Level": "String",
  "Description": "String",
  "Modules": [
    {
      "Title": "String",
      "Content": "String",
      "Order": "Number",
      "EstimatedDuration": "Number"
    }
  ],
  "Quiz": [
    {
      "Title": "String",
      "EstimatedDuration": "Number",
      "PassingRate": "Number",
      "QuizQuestions": [
        {
          "Title": "String",
          "Content": "String",
          "Order": "Number",
          "QuizQuestionOptions": [
            {
              "Content": "String",
              "IsAnswer": "Boolean",
              "Order": "Number"
            }]
        }]
    }
  ],
  "ToPublish": "Boolean",
  "CoverImageUpload": "File"
}
```

**Response:**

200 Success

```json
{
  "Course": {
    "Id": "String",
    "Title": "String",
    "Subtitle": "String",
    "coursecategories": "[String]",
    "Level": "String",
    "Description": "String",
    "UrlName": "String",
    "ItemDefaultUrl": "String",
    "coursepublishingstatuses": "[String]"
  },
  "CoverImage": {
    "Url": "String",
    "Width": "Number",
    "Height": "Number",
    "AlternativeText": "String"
  }
}
```

## Update a Course

**URL:** `/api/Course`

**Method:** PUT

**Auth required:** Yes

**Permissions:** Course can only be updated by its Owner

**Body:**

```json
{
  "Id": "String",
  "Title": "String",
  "Subtitle": "String",
  "Category": "String",
  "Level": "String",
  "Description": "String",
  "Modules": [
    {
      "Id": "String",
      "Title": "String",
      "Content": "String",
      "Order": "Number",
      "EstimatedDuration": "Number",
      "ToDelete": "Boolean"
    }
  ],
  "Quiz": [
    {
      "Id": "String",
      "Title": "String",
      "EstimatedDuration": "Number",
      "PassingRate": "Number",
      "QuizQuestions": [
        {
          "Id": "String",
          "Title": "String",
          "Content": "String",
          "Order": "Number",
          "QuizQuestionOptions": [
            {
              "Id": "String",
              "Content": "String",
              "IsAnswer": "Boolean",
              "Order": "Number",
              "ToDelete": "Boolean"
            }],
          "ToDelete": "Boolean"
        }]
    }
  ],
  "ToArchive": "Boolean",
  "CoverImageUpload": "File"
}
```

**Response:**

200 Success

```json
{
  "Course": {
    "Id": "String",
    "Title": "String",
    "Subtitle": "String",
    "coursecategories": "[String]",
    "Level": "String",
    "Description": "String",
    "UrlName": "String",
    "ItemDefaultUrl": "String",
    "coursepublishingstatuses": "[String]"
  },
  "CoverImage": {
    "Url": "String",
    "Width": "Number",
    "Height": "Number",
    "AlternativeText": "String"
  }
}
```

## Delete a Course

**URL:** `/api/Course?id=[CourseId]`

**Method:** DELETE

**Auth required:** Yes

**Permissions:** Course can only be deleted by its Owner

**Query params:**

```
"id": "String"
```

**Response:**

200 Success

## See more Courses in Category

**URL:** `/api/Course/courses?categoryId=[CategoryId]&count=[Count]&excludedPostIds=[ExcludedPostIds]`

**Method:** GET

**Auth required:** No

**Query params:**

```
"categoryId": "String"
"count": "Number"
"excludedPostIds": "String,...,String"
```

**Response:**

200 Success: Partial view [CourseViewModelList.cshtml](../Views/Shared/DisplayTemplates/CourseViewModelList.cshtml)

## Search for Courses

**URL:** `/api/Course?searchText=[SearchText]&excludedPostIds=[ExcludedPostIds]`

**Method:** GET

**Auth required:** No

**Query params:**

```
"searchText": "String"
"excludedPostIds": "String,...,String"
```

**Response:**

200 Success: Partial view [CourseViewModelList.cshtml](../Views/Shared/DisplayTemplates/CourseViewModelList.cshtml)

## [View] Add a Module into Course

**URL:** `/api/Course/addModule`

**Method:** GET

**Auth required:** Yes

**Response:**

200 Success: Partial view [ModuleLinkViewModel.cshtml](../Views/Shared/DisplayTemplates/ModuleLinkViewModel.cshtml)

## [View] Add a Question into Course Quiz

**URL:** `/api/Course/addQuestion`

**Method:** GET

**Auth required:** Yes

**Response:**

200 Success: Partial view [QuizQuestionViewModel.cshtml](../Views/Shared/DisplayTemplates/QuizQuestionViewModel.cshtml)

## [View] Add an Option into Quiz Question

**URL:** `/api/Course/addOption`

**Method:** GET

**Auth required:** Yes

**Response:**

200 Success: Partial view [QuizQuestionOptionViewModel.cshtml](../Views/Shared/DisplayTemplates/QuizQuestionOptionViewModel.cshtml)

## Join a Course

**URL:** `/api/Course/joinCourse?Id=[CourseId]`

**Method:** GET

**Auth required:** Yes

**Response:**

200 Success

```json
{
  "ModuleId": "String",
  "QuizId": "String"
}
```

## Mark a Course Module's participation as Completed

**URL:** `/api/Course/markModuleCompleted?Id=[ModuleId]`

**Method:** GET

**Auth required:** Yes

**Response:**

200 Success

```json
{
  "ModuleCompletionId": "String",
  "CompletionDate": "DateTime",
  "RelatedModule": {
    "Id": "String",
    "Title": "String",
    "Content": "String",
    "Order": "Number",
    "EstimatedDuration": "Number"
  }
}
```

## Submit Quiz answers

**URL:** `/api/Course/submitAnswers?Id=[QuizId]`

**Method:** POST

**Auth required:** Yes

**Response:**

200 Success

```json
{
  "QuizQuestions": [
    {
      "Id": "String",
      "QuizQuestionOptions": "[String]"
    }
  ]
}
```
