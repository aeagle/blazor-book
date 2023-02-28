function spaces_updateStyleDefinition(id, definition) {
  const existing = document.getElementById(`style_${id}`);

  if (existing) {
    if (existing.innerHTML !== definition) {
      existing.innerHTML = definition;
    }
  } else {
    const newStyle = document.createElement("style");
    newStyle.id = `style_${id}`;
    newStyle.innerHTML = definition;
    document.head.appendChild(newStyle);
  }
}

function spaces_removeStyleDefinition(id) {
  const existing = document.getElementById(`style_${id}`);
  if (existing) {
    document.head.removeChild(existing);
  }
}

function spaces_getBoundingClientRect(element) {
  return element.getBoundingClientRect();
}

const spaces_eventListeners = [];

function spaces_registerevent(spaceId, eventName) {
  let callback = spaces_eventListeners.find(
    (e) => e.spaceId === spaceId && e.eventName === e.eventName
  );
  if (!callback) {
    callback = (e) => spaces_notifyevent(spaceId, eventName, e);
    spaces_eventListeners.push(callback);
    window.addEventListener(eventName, callback);
  }
}

function spaces_unregisterevent(spaceId, eventName) {
  let callback = spaces_eventListeners.find(
    (e) => e.spaceId === spaceId && e.eventName === e.eventName
  );
  if (callback) {
    window.removeEventListener(eventName, callback);
  }
}

function spaces_notifyevent(spaceId, eventName, eventArgs) {
  let args = {};

  switch (eventName) {
    case "mousemove":
    case "mouseup":
      args = { clientX: eventArgs.clientX, clientY: eventArgs.clientY };
      break;
    case "touchmove":
    case "touchend":
      args = { touches: eventArgs.touches };
      break;
  }

  DotNet.invokeMethodAsync(
    "BlazorSpaces",
    "spaces_notifyevent",
    spaceId,
    eventName,
    args
  );
}
