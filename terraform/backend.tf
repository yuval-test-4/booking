terraform {
  backend "s3" {
    bucket = "terraform-state-demonstration"
    key    = "development/booking"
    region = "us-east-1"
  }
}