# Setup
see ([https://cloud.google.com/vertex-ai/docs/start/client-libraries](https://cloud.google.com/vertex-ai/docs/start/client-libraries#before_you_begin))

1. Create a Google Cloud project
2. Enable Vertex AI API
3. Create a service account (https://console.cloud.google.com/projectselector/iam-admin/serviceaccounts/create?supportedpurview=project&_ga=2.57585964.70120560.1714397179-1812577812.1713430694)
4. Create and download a service account key
5. Insert your project id into Gemini ProjectId variable.
6. Insert your path to the service account key inside Gemini CredentialsPath variable.

# Run invoice digitalizer

To run invoice digitalizer pass filePath of your invoice img into GetDigitalizedDataAsync method.
