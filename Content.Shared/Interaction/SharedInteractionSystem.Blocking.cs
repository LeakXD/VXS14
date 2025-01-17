using Content.Shared.Hands;
using Content.Shared.Interaction.Components;
using Content.Shared.Interaction.Events;
using Content.Shared.Item;
using Content.Shared.Movement.Events;

namespace Content.Shared.Interaction;

public partial class SharedInteractionSystem
{
    public void InitializeBlocking()
    {
        SubscribeLocalEvent<BlockMovementComponent, UpdateCanMoveEvent>(OnMoveAttempt);
        SubscribeLocalEvent<BlockMovementComponent, UseAttemptEvent>(CancelEvent);
        SubscribeLocalEvent<BlockMovementComponent, InteractionAttemptEvent>(CancelEvent);
        SubscribeLocalEvent<BlockMovementComponent, DropAttemptEvent>(CancelEvent);
        SubscribeLocalEvent<BlockMovementComponent, PickupAttemptEvent>(CancelEvent);
        SubscribeLocalEvent<BlockMovementComponent, ChangeDirectionAttemptEvent>(CancelEvent);

        SubscribeLocalEvent<BlockMovementComponent, ComponentStartup>(OnBlockingStartup);
        SubscribeLocalEvent<BlockMovementComponent, ComponentShutdown>(OnBlockingShutdown);
    }

    private void OnMoveAttempt(EntityUid uid, BlockMovementComponent component, UpdateCanMoveEvent args)
    {
        if (component.LifeStage > ComponentLifeStage.Running)
            return;

        args.Cancel(); // no more scurrying around
    }

    private void CancelEvent(EntityUid uid, BlockMovementComponent component, CancellableEntityEventArgs args)
    {
        args.Cancel();
    }

    private void OnBlockingStartup(EntityUid uid, BlockMovementComponent component, ComponentStartup args)
    {
        _actionBlockerSystem.UpdateCanMove(uid);
    }

    private void OnBlockingShutdown(EntityUid uid, BlockMovementComponent component, ComponentShutdown args)
    {
        _actionBlockerSystem.UpdateCanMove(uid);
    }
}

