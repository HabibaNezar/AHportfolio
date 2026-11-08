using PortfolioAPI.Models;

namespace PortfolioAPI.Data;

public static class AboutSeed
{
    public static About Create()
    {
        var about = new About
        {
            Text = "AI-focused Python Developer specializing in LLM applications, RAG systems, and multi-agent architectures. AWS Certified. Built and shipped a production, end-to-end agentic AI solution for one of Egypt's most popular e-commerce brands, serving users across Meta channels (WhatsApp, Messenger, Instagram) and a website widget. Also delivered RAG pipelines and Botpress chatbots, and built stateful multi-agent workflows with LangGraph. Ranked Top 10 in an Arabic Speech Recognition competition, with a strong focus on cost efficiency and low-latency inference."
        };

        about.SkillCategories.AddRange(new[]
        {
            new SkillCategory { Name = "Languages", Description = "Programming and database languages", Items = { new("Python", "python-icon"), new("SQL (PostgreSQL, MySQL)", "database-icon"), new("OOP", "code-icon") } },
            new SkillCategory { Name = "AI / ML", Description = "Machine Learning and Deep Learning frameworks", Items = { new("PyTorch", "pytorch-icon"), new("Scikit-Learn", "sklearn-icon"), new("Hugging Face Transformers", "hf-icon"), new("Pandas", "pandas-icon"), new("TensorFlow", "tf-icon") } },
            new SkillCategory { Name = "GenAI & LLMs", Description = "Generative AI, Agentic workflows, and LLM orchestration", Items = { new("RAG", "rag-icon"), new("LangChain", "langchain-icon"), new("LangGraph", "langgraph-icon"), new("CrewAI", "crewai-icon"), new("DSPy", "dspy-icon"), new("Ollama", "ollama-icon"), new("OpenAI API", "openai-icon"), new("Claude API", "claude-icon") } },
            new SkillCategory { Name = "LLM Techniques", Description = "Prompt engineering, fine-tuning, and retrieval optimization", Items = { new("Prompt Engineering", "prompt-icon"), new("Fine-Tuning (LoRA, QLoRA)", "tuning-icon"), new("Semantic Caching", "cache-icon"), new("Reranking", "rerank-icon"), new("Vector Search Optimization", "search-icon") } },
            new SkillCategory { Name = "Vector Databases", Description = "Vector stores and retrieval engines", Items = { new("Qdrant", "qdrant-icon"), new("PGvector", "pgvector-icon"), new("FAISS", "faiss-icon"), new("Chroma", "chroma-icon") } },
            new SkillCategory { Name = "MLOps & Cloud", Description = "Cloud deployment, containerization, and API backends", Items = { new("Docker", "docker-icon"), new("AWS (ECS, Lambda, S3)", "aws-icon"), new("FastAPI", "fastapi-icon"), new("MLflow", "mlflow-icon") } },
            new SkillCategory { Name = "Monitoring & Eval", Description = "LLM evaluation and observability tools", Items = { new("LangFuse", "langfuse-icon"), new("Arize Phoenix", "phoenix-icon") } },
            new SkillCategory { Name = "Tools", Description = "Developer platforms and utilities", Items = { new("Botpress", "botpress-icon"), new("MCP", "mcp-icon"), new("Git", "git-icon"), new("Gradio", "gradio-icon"), new("Jupyter", "jupyter-icon"), new("Linux", "linux-icon") } }
        });

        about.Experiences.AddRange(new[]
        {
            new Experience
            {
                From = new DateTime(2026, 4, 1),
                To = new DateTime(2026, 8, 12),
                Title = "AI Engineer at Trigz Ai",
                Description = "Built multi-agent AI systems with LangGraph integrated into a SaaS platform across Magento, Salla, and Zid; delivered an AI WhatsApp chatbot via Meta API with <2.5s end-to-end response time and OCR pipelines for structured data extraction. Engineered end-to-end document automation workflows with pgvector, Celery, and advanced prompt management, integrating OCR outputs into scalable agentic pipelines."
            },
            new Experience
            {
                From = new DateTime(2024, 3, 1),
                To = new DateTime(2024, 8, 1),
                Title = "AI Engineer at MT College",
                Description = "Built an Arabic Speech-to-Text system (SpeechBrain, MFCC) on 50K+ audio samples, achieving MLT 40.71 and Top 10 out of 50+ teams in 2024 national competition. Integrated Speaker Diarization using pyannote 3.1 for multi-speaker segmentation in NLP pipelines. Fine-tuned an Arabic Text Summarization model (Hugging Face, TensorFlow) on 10K+ articles, achieving ROUGE-L 0.42 and Top 15 in 2023 national competition."
            },
            new Experience
            {
                From = new DateTime(2023, 1, 1),
                To = new DateTime(2023, 11, 1),
                Title = "AI Engineer Intern at Instant",
                Description = "Built ML/Deep Learning skills in model training, evaluation, and optimization while collaborating with senior engineers. Familiar with core NLP techniques including tokenization, embeddings, and sequence modeling. Applied data preprocessing and feature engineering to improve model performance."
            }
        });

        return about;
    }
}
