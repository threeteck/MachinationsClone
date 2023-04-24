using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MachinationsClone.Models.DTOs.Request;
using MachinationsClone.Models.DTOs.Response;
using MachinationsClone.Models.Entities.Geometry;
using MachinationsClone.Models.Entities.Graph;
using MachinationsClone.Models.Machinations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MachinationsClone.Controllers
{
    [Route("/api/graph")]
    [Authorize]
    [ApiController]
    public class GraphController : Controller
    {
        private readonly ApplicationContext _context;

        public GraphController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateGraphDto dto)
        {
            var graph = new Graph
            {
                Name = dto.Name,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                StepSize = 1,
                UserId = User.GetId()
            };
            await _context.Graphs.AddAsync(graph);
            await _context.SaveChangesAsync();
            return new OkObjectResult(graph.Id);
        }

        [HttpPost("update/{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGraphDto dto)
        {
            var userId = User.GetId();
            var graph = await _context.Graphs.FirstOrDefaultAsync(g => g.Id == id && g.UserId == userId);
            if (graph == null)
            {
                return new NotFoundResult();
            }

            graph.Name = dto.Name;
            graph.Description = dto.Description;
            graph.StepSize = dto.StepSize;
            graph.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return new OkResult();
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.GetId();
            var graphs = await _context.Graphs
                .Where(g => g.UserId == userId)
                .Select(g => GraphDto.FromGraph(g))
                .ToListAsync();

            return new OkObjectResult(graphs);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var userId = User.GetId();
            var graph = await _context.Graphs
                .Where(g => g.Id == id && g.UserId == userId)
                .Include(g => g.GraphElements)
                .Include(g => g.CurrentState)
                .Select(g => GraphDto.FromGraph(g))
                .FirstOrDefaultAsync();

            if (graph == null)
                return new NotFoundResult();

            return new JsonResult(graph);
        }

        [HttpPost("{id:guid}/delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.GetId();
            var graph = await _context.Graphs.FirstOrDefaultAsync(g =>
                g.Id == id && g.UserId == userId);
            if (graph == null)
            {
                return new NotFoundResult();
            }

            _context.Graphs.Remove(graph);
            await _context.SaveChangesAsync();
            return new OkResult();
        }

        [HttpGet("{id:guid}/geometry")]
        public async Task<IActionResult> GetGeometry(Guid id)
        {
            var nodeGeometry = await _context.GraphNodes
                .Where(ge => ge.GraphId == id)
                .Include(ge => ge.NodeGeometry)
                .Select(ge => NodeGeometryDto.FromNodeGeometry(ge.NodeGeometry))
                .ToListAsync();

            return new OkObjectResult(nodeGeometry);
        }

        [HttpGet("{id:guid}/currentState")]
        public async Task<IActionResult> GetCurrentState(Guid id)
        {
            var userId = User.GetId();
            var graph = await _context.Graphs
                .Where(g => g.Id == id && g.UserId == userId)
                .Include(g => g.CurrentState)
                .FirstOrDefaultAsync();
            
            if (graph == null)
                return new NotFoundResult();
            
            var state = graph.CurrentState;
            if (state == null)
                return new NotFoundResult();
            
            var stateDto = GraphStateDto.FromGraphState(state);

            return new OkObjectResult(stateDto);
        }
        
        [HttpGet("{id:guid}/statesGroups")]
        public async Task<IActionResult> GetStatesGroups(Guid id)
        {
            var statesGroups = await _context.GraphStatesGroups
                .Where(gsg => gsg.GraphId == id)
                .Select(gsg => GraphStatesGroupDto.FromGraphStatesGroup(gsg))
                .ToListAsync();

            return new OkObjectResult(statesGroups);
        }

        [HttpPost("{id:guid}/setStatesGroup")]
        public async Task<IActionResult> SetStatesGroup(Guid id, Guid statesGroupId)
        {
            var userId = User.GetId();
            var graph = await _context.Graphs.FirstOrDefaultAsync(g =>
                g.Id == id && g.UserId == userId);
            if (graph == null)
            {
                return new NotFoundResult();
            }
            
            var statesGroup = await _context.GraphStatesGroups.FirstOrDefaultAsync(gsg =>
                gsg.Id == statesGroupId && gsg.GraphId == id);
            if (statesGroup == null)
            {
                return new NotFoundResult();
            }

            graph.CurrentStatesGroupId = statesGroupId;
            graph.CurrentStateId = statesGroup.LastStateId;
            graph.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            
            var state = await _context.GraphStates.FirstOrDefaultAsync(gs => gs.Id == graph.CurrentStateId);
            var stateDto = GraphStateDto.FromGraphState(state);
            return new OkObjectResult(stateDto);
        }

        [HttpPost("{id:guid}/resetState")]
        public async Task<IActionResult> ResetState(Guid id)
        {
            var userId = User.GetId();
            var graph = await _context.Graphs.Where(g => g.Id == id && g.UserId == userId)
                .Include(g => g.GraphElements)
                .FirstOrDefaultAsync();

            if (graph == null)
            {
                return new NotFoundResult();
            }

            var machinations = new MachinationsGraph(graph.Id);
            machinations.CompileGraph(graph.GraphElements);
            machinations.InitializeState();
            var state = machinations.GetState(DateTime.UtcNow);
            
            var statesGroupCount = await _context.GraphStatesGroups.CountAsync(gsg => gsg.GraphId == id);
            
            var statesGroup = new GraphStatesGroup()
            {
                GraphId = id,
                CreatedAt = DateTime.UtcNow,
                LastStateId = state.Id,
                LastState = state,
                Order = statesGroupCount,
                TotalSteps = 1
            };
            
            state.GraphStatesGroupId = statesGroup.Id;
            state.Step = 0;
            
            await _context.GraphStatesGroups.AddAsync(statesGroup);
            await _context.GraphStates.AddAsync(state);
            await _context.SaveChangesAsync();
            
            graph.CurrentStatesGroupId = statesGroup.Id;
            graph.CurrentStateId = state.Id;
            graph.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            
            var stateDto = GraphStateDto.FromGraphState(state);
            return new OkObjectResult(stateDto);
        }

        [HttpPost("{id:guid}/addNode")]
        public async Task<IActionResult> AddNode(Guid id, [FromBody] AddNodeDto dto)
        {
            var userId = User.GetId();
            var graph = await _context.Graphs.FirstOrDefaultAsync(g =>
                g.Id == id && g.UserId == userId);
            if (graph == null)
            {
                return new NotFoundResult();
            }

            var graphNode = new GraphNode()
            {
                GraphId = id,
                Name = dto.Name,
                ActivationMode = ActivationMode.GetDefaultEnum(),
                PullMode = PullMode.GetDefaultEnum(),
                NodeTypeName = dto.NodeTypeName,
                Properties = MachinationsFactory.GetDefaultProperties(dto.NodeTypeName)
            };

            await _context.GraphNodes.AddAsync(graphNode);
            await _context.SaveChangesAsync();

            var nodeGeometry = new NodeGeometry()
            {
                GraphNodeId = graphNode.Id,
                X = dto.X,
                Y = dto.Y
            };

            await _context.NodeGeometries.AddAsync(nodeGeometry);
            await _context.SaveChangesAsync();

            var graphElementDto = GraphNodeDto.FromGraphNode(graphNode, nodeGeometry);

            return new OkObjectResult(graphElementDto);
        }

        [HttpPost("{id:guid}/addConnection")]
        public async Task<IActionResult> AddConnection(Guid id, [FromBody] AddConnectionDto dto)
        {
            var userId = User.GetId();
            var graph = await _context.Graphs.FirstOrDefaultAsync(g =>
                g.Id == id && g.UserId == userId);
            if (graph == null)
            {
                return new NotFoundResult();
            }
            
            if (dto.StartId == dto.EndId && dto.StartId != null)
            {
                return new BadRequestResult();
            }
            
            dto.StartId = dto.StartId == Guid.Empty ? null : dto.StartId;
            dto.EndId = dto.EndId == Guid.Empty ? null : dto.EndId;
            
            if (dto.StartId == null && dto.EndId == null)
            {
                return new BadRequestResult();
            }
            
            if (dto.StartId != null)
            {
                var startNode = await _context.GraphNodes.FirstOrDefaultAsync(n => n.Id == dto.StartId);
                if (startNode == null)
                {
                    return new BadRequestResult();
                }
            }
            
            if (dto.EndId != null)
            {
                var endNode = await _context.GraphNodes.FirstOrDefaultAsync(n => n.Id == dto.EndId);
                if (endNode == null)
                {
                    return new BadRequestResult();
                }
            }

            var graphConnection = new GraphConnection()
            {
                GraphId = id,
                ConnectionTypeName = dto.ConnectionTypeName,
                StartId = dto.StartId,
                EndId = dto.EndId,
                Properties = MachinationsFactory.GetDefaultProperties(dto.ConnectionTypeName)
            };

            await _context.GraphConnections.AddAsync(graphConnection);
            await _context.SaveChangesAsync();

            var graphElementDto = GraphConnectionDto.FromGraphConnection(graphConnection);
            return new OkObjectResult(graphElementDto);
        }
        
        [HttpPost("{id:guid}/updateNode")]
        public async Task<IActionResult> UpdateNode(Guid id, [FromBody] UpdateNodeDto dto)
        {
            var userId = User.GetId();
            var graph = await _context.Graphs.FirstOrDefaultAsync(g =>
                g.Id == id && g.UserId == userId);
            if (graph == null)
            {
                return new NotFoundResult();
            }

            var graphNode = await _context.GraphNodes.FirstOrDefaultAsync(n => n.Id == dto.Id);
            if (graphNode == null)
            {
                return new NotFoundResult();
            }

            if (dto.Name != null)
                graphNode.Name = dto.Name;
            if (dto.ActivationMode != null)
                graphNode.ActivationMode = dto.ActivationMode.Value;
            if (dto.PullMode != null)
                graphNode.PullMode = dto.PullMode.Value;
            graphNode.Properties = graphNode.Properties.Merge(dto.Properties);
            await _context.SaveChangesAsync();

            var graphElementDto = GraphNodeDto.FromGraphNode(graphNode);
            return new JsonResult(graphElementDto);
        }
        
        [HttpPost("{id:guid}/updateNodeGeometry")]
        public async Task<IActionResult> UpdateNodeGeometry(Guid id, [FromBody] UpdateNodeGeometryDto dto)
        {
            var nodeGeometry = await _context.NodeGeometries.FirstOrDefaultAsync(n => n.GraphNodeId == dto.NodeId);
            if (nodeGeometry == null)
            {
                return new NotFoundResult();
            }

            if (dto.X != null)
                nodeGeometry.X = dto.X.Value;
            if (dto.Y != null)
                nodeGeometry.Y = dto.Y.Value;
            await _context.SaveChangesAsync();

            var nodeGeometryDto = NodeGeometryDto.FromNodeGeometry(nodeGeometry);
            return new OkObjectResult(nodeGeometryDto);
        }
        
        [HttpPost("{id:guid}/updateConnection")]
        public async Task<IActionResult> UpdateConnection(Guid id, [FromBody] UpdateConnectionDto dto)
        {
            var userId = User.GetId();
            var graph = await _context.Graphs.FirstOrDefaultAsync(g =>
                g.Id == id && g.UserId == userId);
            if (graph == null)
            {
                return new NotFoundResult();
            }

            var graphConnection = await _context.GraphConnections.FirstOrDefaultAsync(c => c.Id == dto.Id);
            if (graphConnection == null)
            {
                return new NotFoundResult();
            }
            
            if (dto.StartId == dto.EndId && dto.StartId != null)
            {
                return new BadRequestResult();
            }
            
            dto.StartId = dto.StartId == Guid.Empty ? null : dto.StartId;
            dto.EndId = dto.EndId == Guid.Empty ? null : dto.EndId;

            if (dto.StartId != null)
            {
                var startNode = await _context.GraphNodes.FirstOrDefaultAsync(n => n.Id == dto.StartId);
                if (startNode == null)
                {
                    return new BadRequestResult();
                }
                graphConnection.StartId = dto.StartId;
            }
            
            if (dto.EndId != null)
            {
                var endNode = await _context.GraphNodes.FirstOrDefaultAsync(n => n.Id == dto.EndId);
                if (endNode == null)
                {
                    return new BadRequestResult();
                }
                graphConnection.EndId = dto.EndId;
            }
            
            graphConnection.Properties = graphConnection.Properties.Merge(dto.Properties);
            await _context.SaveChangesAsync();

            var graphElementDto = GraphConnectionDto.FromGraphConnection(graphConnection);
            return new JsonResult(graphElementDto);
        }
        
        [HttpPost("{id:guid}/deleteNode")]
        public async Task<IActionResult> DeleteNode(Guid id, [FromBody] DeleteNodeDto dto)
        {
            var userId = User.GetId();
            var graph = await _context.Graphs.FirstOrDefaultAsync(g =>
                g.Id == id && g.UserId == userId);
            if (graph == null)
            {
                return new NotFoundResult();
            }

            var graphNode = await _context.GraphNodes.FirstOrDefaultAsync(n => n.Id == dto.NodeId);
            if (graphNode == null)
            {
                return new NotFoundResult();
            }

            _context.GraphNodes.Remove(graphNode);
            await _context.SaveChangesAsync();

            return new OkResult();
        }
        
        [HttpPost("{id:guid}/deleteConnection")]
        public async Task<IActionResult> DeleteConnection(Guid id, [FromBody] DeleteConnectionDto dto)
        {
            var userId = User.GetId();
            var graph = await _context.Graphs.FirstOrDefaultAsync(g =>
                g.Id == id && g.UserId == userId);
            if (graph == null)
            {
                return new NotFoundResult();
            }

            var graphConnection = await _context.GraphConnections.FirstOrDefaultAsync(c => c.Id == dto.ConnectionId);
            if (graphConnection == null)
            {
                return new NotFoundResult();
            }

            _context.GraphConnections.Remove(graphConnection);
            await _context.SaveChangesAsync();

            return new OkResult();
        }
        
        [HttpPost("{id:guid}/run")]
        public async Task<IActionResult> Run(Guid id, [FromBody] RunDto dto)
        {
            var userId = User.GetId();
            var graph = await _context.Graphs.Where(g => g.Id == id && g.UserId == userId)
                .Include(g => g.GraphElements)
                .FirstOrDefaultAsync();

            if (graph == null)
            {
                return new NotFoundResult();
            }

            var machinations = new MachinationsGraph(graph.Id);
            machinations.CompileGraph(graph.GraphElements);

            if (graph.CurrentStatesGroupId == null)
            {
                machinations.InitializeState();
                var state = machinations.GetState(DateTime.UtcNow);
                
                var statesGroup = new GraphStatesGroup()
                {
                    GraphId = id,
                    CreatedAt = DateTime.UtcNow,
                    LastStateId = state.Id,
                    LastState = state,
                    Order = 0,
                    TotalSteps = 1
                };
            
                state.GraphStatesGroupId = statesGroup.Id;
                state.Step = 0;
            
                await _context.GraphStatesGroups.AddAsync(statesGroup);
                await _context.GraphStates.AddAsync(state);
                await _context.SaveChangesAsync();
            
                graph.CurrentStatesGroupId = statesGroup.Id;
                graph.CurrentStateId = state.Id;
                graph.UpdatedAt = DateTime.UtcNow;
            
                await _context.SaveChangesAsync();
            }
            else if (graph.CurrentStatesGroupId != null && graph.CurrentStateId == null)
            {
                var statesGroup = await _context.GraphStatesGroups.FirstOrDefaultAsync(gsg =>
                    gsg.Id == graph.CurrentStatesGroupId && gsg.GraphId == id);
                if (statesGroup == null)
                {
                    return new NotFoundResult();
                }
                
                graph.CurrentStateId = statesGroup.LastStateId;
                graph.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
            
            var currentState = await _context.GraphStates.FirstOrDefaultAsync(s =>
                s.Id == graph.CurrentStateId && s.GraphId == id);
            
            if (currentState == null)
                throw new Exception("Current state not found");
            
            machinations.LoadState(currentState);
            
            GraphState newState = null;
            for (int i = 0; i < dto.StepCount; i++)
            {
                int currentStep = currentState.Step + i;
                machinations.Run(currentStep);
                newState = machinations.GetState(DateTime.UtcNow);
                newState.GraphStatesGroupId = graph.CurrentStatesGroupId;
                newState.Step = currentState.Step + i + 1;
                await _context.GraphStates.AddAsync(newState);
            }
            
            await _context.SaveChangesAsync();
            
            if (newState == null)
                throw new Exception("New state not found");
            
            graph.CurrentStateId = newState.Id;
            graph.UpdatedAt = DateTime.UtcNow;
            
            var currentStatesGroup = await _context.GraphStatesGroups.FirstOrDefaultAsync(gsg =>
                gsg.Id == graph.CurrentStatesGroupId && gsg.GraphId == id);
            
            currentStatesGroup.LastStateId = newState.Id;
            currentStatesGroup.TotalSteps = newState.Step + 1;
            
            await _context.SaveChangesAsync();
            
            var newStateDto = GraphStateDto.FromGraphState(newState);
            return new JsonResult(newStateDto);
        }

        [HttpPost("{id:guid}/exportStatesGroup")]
        public async Task<IActionResult> ExportStatesGroup(Guid id)
        {
            var userId = User.GetId();
            var graph = await _context.Graphs.FirstOrDefaultAsync(g =>
                g.Id == id && g.UserId == userId);
            if (graph == null)
            {
                return new NotFoundResult();
            }

            var states = await _context.GraphStates
                .Where(s => s.GraphId == id && s.GraphStatesGroupId == graph.CurrentStatesGroupId)
                .ToListAsync();
            
            states.Sort( (s1, s2) => s1.Step.CompareTo(s2.Step) );
            var statesDto = states.Select(s => GraphStateDto.FromGraphState(s)).ToList();
            
            return new OkObjectResult(statesDto);
        }
        
        [HttpGet("nodeTypes")]
        public async Task<IActionResult> GetNodeTypes()
        {
            var nodeTypes = await _context.NodeTypes.ToListAsync();
            var nodeTypesDto = nodeTypes.Select(nt => NodeTypeDto.FromNodeType(nt)).ToList();
            return new OkObjectResult(nodeTypesDto);
        }
        
        [HttpGet("connectionTypes")]
        public async Task<IActionResult> GetConnectionTypes()
        {
            var connectionTypes = await _context.ConnectionTypes.ToListAsync();
            var connectionTypesDto = connectionTypes.Select(ct => ConnectionTypeDto.FromConnectionType(ct)).ToList();
            return new OkObjectResult(connectionTypesDto);
        }
    }
}