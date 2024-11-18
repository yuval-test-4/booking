module "ecs_service_booking" {
  source  = "terraform-aws-modules/ecs/aws//modules/service"
  version = "5.2.2"

  name        = "booking"
  cluster_arn = module.ecs_cluster_booking.arn

  cpu    = 1024
  memory = 4096

  container_definitions = {
    ("booking") = {
      essential = true
      cpu       = 512
      memory    = 1024
      image     = module.ecr_booking.repository_url

      port_mappings = [
        {
          name          = "booking"
          containerPort = 8080
          hostPort      = 8080
          protocol      = "tcp"
        }
      ]

      readonly_root_filesystem  = false
      enable_cloudwatch_logging = false

      log_configuration = {
        logDriver = "awslogs"
        options = {
          awslogs-create-group  = "true"
          awslogs-group         = "/ecs/booking"
          awslogs-region        = local.region
          awslogs-stream-prefix = "ecs"
        }
      }

      memory_reservation = 100
    }
  }

  load_balancer = {
    service = {
      target_group_arn = element(module.ecs_alb_booking.target_group_arns, 0)
      container_name   = "booking"
      container_port   = 8080
    }
  }

  subnet_ids = module.vpc.private_subnets

  security_group_rules = {
    alb_ingress = {
      type                     = "ingress"
      from_port                = 8080
      to_port                  = 8080
      protocol                 = "tcp"
      source_security_group_id = module.ecs_alb_sg_booking.security_group_id
    }
    egress_all = {
      type        = "egress"
      from_port   = 0
      to_port     = 0
      protocol    = "-1"
      cidr_blocks = ["0.0.0.0/0"]
    }
  }
}

resource "aws_service_discovery_http_namespace" "booking" {
  name = "booking"
}

output "service_id_booking" {
  description = "ARN that identifies the service"
  value       = module.ecs_service_booking.id
}

output "service_name_booking" {
  description = "Name of the service"
  value       = module.ecs_service_booking.name
}

output "service_iam_role_name_booking" {
  description = "Service IAM role name"
  value       = module.ecs_service_booking.iam_role_name
}

output "service_iam_role_arn_booking" {
  description = "Service IAM role ARN"
  value       = module.ecs_service_booking.iam_role_arn
}

output "service_iam_role_unique_id_booking" {
  description = "Stable and unique string identifying the service IAM role"
  value       = module.ecs_service_booking.iam_role_unique_id
}

output "service_container_definitions_booking" {
  description = "Container definitions"
  value       = module.ecs_service_booking.container_definitions
}

output "service_task_definition_arn_booking" {
  description = "Full ARN of the Task Definition (including both `family` and `revision`)"
  value       = module.ecs_service_booking.task_definition_arn
}
